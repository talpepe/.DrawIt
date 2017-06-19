using DrawIt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DrawIt.Controllers
{
    [Authorize]
    public class MembersController : Controller
    {
        //
        // GET: /Members/
        [Authorize]

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Draw()
        {
            return View();
        }

        public ActionResult ShowRooms()
        {
            return View(new RoomModels());
        }

        public ActionResult CreateRoom()
        {
            return View();
        }

    }
}
