using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using DrawIt3.Models;
using System.Threading.Tasks;
using System.Timers;
using System.Threading;
using System.Diagnostics;

namespace DrawIt3.Hubs
{
    public class DrawHub : Hub
    {

             System.Timers.Timer aTimer = new System.Timers.Timer();
             int i=1;
             int numWords;
             Random rnd = new Random();
             Stopwatch stopWatch = new Stopwatch();
            
        public void EndGame()
        {
            aTimer.Close();

            var name = Context.User.Identity.Name;
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.SingleOrDefault(u => u.UserName == name);
                var room = db.Rooms.SingleOrDefault(u => u.RoomName == user.UsingRoom);
                Clients.Group(user.UsingRoom).alertAll("Game has ended, thank you for playing! \n Room is now closed");

                db.Rooms.Remove(room);
                Clients.Group(user.UsingRoom).moveOut();
                db.SaveChanges();


            }

        }




        public void UpdateCanvas(int x, int y, int lastx, int lasty, int size, string color)
        {
            var name = Context.User.Identity.Name;
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.SingleOrDefault(u => u.UserName == name);

            
            Clients.Group(user.UsingRoom).updateDot(x, y, lastx, lasty, size, color);

            }
        }
        public void ClearCanvas()
        {
            var name = Context.User.Identity.Name;
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.SingleOrDefault(u => u.UserName == name);

                Clients.Group(user.UsingRoom).cleanCanvas();
            }
        }

        public string getDrawerName()
        {
            var name = Context.User.Identity.Name;
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.SingleOrDefault(u => u.UserName == name);
                var room = db.Rooms.SingleOrDefault(u => u.RoomName == user.UsingRoom);
                return room.drawer.UserName;
            }
        }

        public void begin()
        {

            var name = Context.User.Identity.Name;
            
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.SingleOrDefault(u => u.UserName == name);


                var room = db.Rooms.SingleOrDefault(u => u.RoomName == user.UsingRoom);



                room.drawer = room.RoomUsers[0];
                room.currentDrawerIndex = 0;
                resetScore(room);
                
                db.SaveChanges();
                numWords =  db.Words.Count();
                aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 15000;

            aTimer.Enabled = true;
            Clients.Group(user.UsingRoom).startDraw();

                Thread.Sleep(1000);
                Clients.Group(user.UsingRoom).alrt(room.RoomUsers[0].UserName);

                chooseDrawer();
            }
        }


        public override Task OnConnected()
        {
            var name = Context.User.Identity.Name;



            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.SingleOrDefault(u => u.UserName == name);

                user.connection = Context.ConnectionId;
                Groups.Add(Context.ConnectionId, user.UsingRoom);
                db.SaveChanges();

            }
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            /*  if (stopCalled)
              {
                  // We know that Stop() was called on the client,
                  // and the connection shut down gracefully.


              }
              else
              {
                  // This server hasn't heard from the client in the last ~35 seconds.
                  // If SignalR is behind a load balancer with scaleout configured, 
                  // the client may still be connected to another SignalR server.
              }*/

            var name = Context.User.Identity.Name;

            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.SingleOrDefault(u => u.UserName == name);

                if (user.UsingRoom != null && Context.ConnectionId != null)
                {
                    Groups.Remove(Context.ConnectionId, user.UsingRoom);
                  //  user.connection = null;
                    //  db.Rooms.SingleOrDefault(u => u.RoomName == user.UsingRoom).RoomUsers.Remove(user);
                   // user.UsingRoom = null;
                    
                    db.SaveChanges(); 
                }
            }



            return base.OnDisconnected(stopCalled);
        }


        public  void chooseDrawer()
        {
            var name = Context.User.Identity.Name;

            stopWatch.Reset();
            int randNum = rnd.Next(0, numWords - 1);


            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.SingleOrDefault(u => u.UserName == name);
                var room = db.Rooms.SingleOrDefault(u => u.RoomName == user.UsingRoom);



                if (room.RoomUsers.Count > room.currentDrawerIndex)
                {
                    stopWatch.Start();
                    room.currentWord = db.Words.SingleOrDefault(u => u.Id == randNum).word;


                    room.drawer = room.RoomUsers[room.currentDrawerIndex];
                    
                   

                    Clients.Group(user.UsingRoom).alrt(room.RoomUsers[room.currentDrawerIndex].UserName);
                    Clients.Client(room.drawer.connection).showWord(room.currentWord);
                  //  Clients.Client(room.drawer.connection).alertAll(room.currentWord);
                    room.currentDrawerIndex++;
                    db.SaveChanges();

                }
                else
                {
                  //  EndGame();
                }

                    


            }




        }

        public void BroadCastMessage(String msgFrom, String msg)
        {
            var name = Context.User.Identity.Name;
            using (var db = new ApplicationDbContext()) 
            { 
                var user = db.Users.SingleOrDefault(u => u.UserName == name);
                var room = db.Rooms.SingleOrDefault(u => u.RoomName == user.UsingRoom);
                if ((room.drawer ==null) && msg.ToLower().Equals(room.currentWord.ToLower()) )
                {
                    int timeRemaining = (int)stopWatch.ElapsedMilliseconds * 1000;
                    user.currentRoomScore += 60 - timeRemaining;

                    Clients.Group(user.UsingRoom).alertAll(msgFrom + " has guessed correctly and awarded " + (60-timeRemaining) + " points!" );
                }
                else
                {
                    
                    Clients.Group(user.UsingRoom).receiveMessage(msgFrom, msg);
                }
            }

           
        }

        private  void OnTimedEvent(object source, ElapsedEventArgs e){
        
            chooseDrawer();
        }

        private void resetScore(Room room)
        {
            using (var db = new ApplicationDbContext())
            {
                foreach (ApplicationUser user in room.RoomUsers)
                {
                    user.currentRoomScore = 0;
                }
            }
            
        }




    }
}