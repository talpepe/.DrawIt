using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DrawIt3.Models
{
    public class RoomUser
    {
        [Key]
        public string UserId { get; set; }
    }
}