using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DrawIt.Models;

namespace DrawIt.Controllers
{
    public class RoomController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize]
        // GET: /Room/
        public ActionResult Index()
        {
            var name = User.Identity.Name;


                var user = db.Users.SingleOrDefault(u => u.UserName == name);

                if (user.UsingRoom != null)
                {
                    db.Rooms.SingleOrDefault(u => u.RoomName == user.UsingRoom).RoomUsers.Remove(user);
                    user.UsingRoom = null;
                }


                db.SaveChanges();




            


            return View(db.Rooms.ToList());
        }


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
              //  RoomModels room = db.Rooms.SingleOrDefault(u => u.RoomName == roomname);
                db.Rooms.SingleOrDefault(u => u.RoomName == roomname).RoomUsers.Add(user);
                user.UsingRoom = id;
                

                db.SaveChanges();
           


            return View(db.Rooms.SingleOrDefault(u => u.RoomName == roomname));
            }
        }




        // GET: /Room/Details/5
         [Authorize]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomModels roommodels = db.Rooms.Find(id);
            if (roommodels == null)
            {
                return HttpNotFound();
            }
            return View(roommodels);
        }

        // GET: /Room/Create
         [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Room/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
         [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="RoomName,Password,MaxUsers")] RoomModels roommodels)
        {
            var name = User.Identity.Name;

            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.UserName == name);
                user.UsingRoom = roommodels.RoomName;
                db.Rooms.Add(roommodels);
          //      roommodels.RoomUsers.Add(user);
                db.SaveChanges();
                return RedirectToAction("WaitingRoom");
            }

            return View(roommodels);
        }

        // GET: /Room/Edit/5
         [Authorize]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomModels roommodels = db.Rooms.Find(id);
            if (roommodels == null)
            {
                return HttpNotFound();
            }
            return View(roommodels);
        }

        // POST: /Room/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include="RoomName,Password,MaxUsers")] RoomModels roommodels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roommodels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(roommodels);
        }

        // GET: /Room/Delete/5
         [Authorize]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomModels roommodels = db.Rooms.Find(id);
            if (roommodels == null)
            {
                return HttpNotFound();
            }
            return View(roommodels);
        }

        // POST: /Room/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(string id)
        {
            RoomModels roommodels = db.Rooms.Find(id);
            db.Rooms.Remove(roommodels);
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
