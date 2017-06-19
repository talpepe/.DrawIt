using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.Identity;
using DrawIt3.Models;
using System.Threading.Tasks;
using System.Data.Entity;

using Microsoft.AspNet.Identity.EntityFramework;



namespace DrawIt3
{
    public class DrawItSignal : Hub
    {

        public void UpdateCanvas(int x, int y, int lastx, int lasty, int size, string color)
        {

            Clients.All.updateDot(x, y, lastx, lasty, size, color);
        }
        public void ClearCanvas()
        {
            Clients.All.clearCanvas();
        }

        public void Start()
        {
            Clients.All.alrt();
            Clients.All.StartDraw();
        }


        public override Task OnConnected()
        {
            var name = Context.User.Identity.Name;



            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.SingleOrDefault(u => u.UserName == name);

                user.connection = Context.ConnectionId;

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

                if (user.UsingRoom != null)
                {
                    user.connection = null;
                    //  db.Rooms.SingleOrDefault(u => u.RoomName == user.UsingRoom).RoomUsers.Remove(user);
                    user.UsingRoom = null;

                    db.SaveChanges();
                }
            }



            return base.OnDisconnected(stopCalled);
        }

    }
}
