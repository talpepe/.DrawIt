using DrawIt3.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DrawIt3.Models
{
    public class Room
    {
        [Key]
        public string RoomName { get; set; }

        public string Password { get; set; }

        public int MaxUsers { get; set; }

        public int CurrentNumPlayers { get; set; }

        public string GameStatus { get; set; }

        public virtual IList<ApplicationUser> RoomUsers { get; set; }

        public int currentDrawerIndex { get; set; }

        public string currentWord { get; set; }

       // public  List<string> RoomUsers { get; set; }

        public ApplicationUser drawer { set; get; }
        public Room()
        {
            RoomUsers = new List<ApplicationUser>();

        }
    }
}