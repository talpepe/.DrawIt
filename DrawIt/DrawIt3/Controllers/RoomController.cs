using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DrawIt3.Models;
using DrawIt3.ViewModels;

namespace DrawIt3.Controllers
{
    public class RoomController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize]
        // GET: /Room/
        public ActionResult Index()
        {
           // var name = User.Identity.Name;


         //   var user = db.Users.SingleOrDefault(u => u.UserName == name);

       //     if (user.UsingRoom != null)
            {
              //  db.Rooms.SingleOrDefault(u => u.RoomName == user.UsingRoom).RoomUsers.Remove(user);
             //   user.UsingRoom = null;
            }


          //  db.SaveChanges();



            return View(db.Rooms.ToList());
        }



        [Authorize]
        public ActionResult Draw()
        {
            var name = User.Identity.Name;
            var user = db.Users.SingleOrDefault(u => u.UserName == name);

            var vm = new RoomViewModel();
            vm.Room = db.Rooms.SingleOrDefault(u => u.RoomName == user.UsingRoom);

            vm.Words = db.Words.ToList();


            return View(vm);
        }



        [Authorize]

        public ActionResult WaitingRoom(string id)
        {
            var name = User.Identity.Name;

            using (var db = new ApplicationDbContext())
            {
                string roomname;
                var user = db.Users.SingleOrDefault(u => u.UserName == name);

                if (id != null)
                { roomname = id; }
                else
                { roomname = user.UsingRoom; }

                
                var room = db.Rooms.SingleOrDefault(u => u.RoomName == roomname);
                if (room == null || user == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                //  RoomModels room = db.Rooms.SingleOrDefault(u => u.RoomName == roomname);
                if (room.RoomUsers.SingleOrDefault(x => x.UserName == name) == null)
                {
                    room.RoomUsers.Add(user);
                }
                if (id != null) { 
                user.UsingRoom = id;
                }


                db.SaveChanges();



                return View(db.Rooms.SingleOrDefault(u => u.RoomName == roomname));
            }
        }

        [Authorize]
        // GET: /Room/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }
        [Authorize]
        // GET: /Room/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Room/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="RoomName,Password,MaxUsers")] Room room)
        {
            var name = User.Identity.Name;

            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.UserName == name);
                user.UsingRoom = room.RoomName;
                db.Rooms.Add(room);
                db.SaveChanges();
                return RedirectToAction("WaitingRoom");
            }

            return View(room);
        }
        [Authorize]
        // GET: /Room/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }
        [Authorize]
        // POST: /Room/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="RoomName,Password,MaxUsers")] Room room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(room);
        }
        [Authorize]
        // GET: /Room/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }
        [Authorize]
        // POST: /Room/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Room room = db.Rooms.Find(id);
            db.Rooms.Remove(room);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
