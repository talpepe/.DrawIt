using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DrawIt.Models
{
    public class RoomModels
    {
        [Key]
        public string RoomName { get; set; }

        public string Password  {get; set;}

        public int MaxUsers { get; set; }

        public virtual ICollection<ApplicationUser> RoomUsers { get; set; }

       // public  List<string> RoomUsers { get; set; }
        public RoomModels(){
            RoomUsers= new List<ApplicationUser>();

    }
    }
}