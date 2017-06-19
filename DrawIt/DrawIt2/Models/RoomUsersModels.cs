using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DrawIt.Models
{
    public class RoomUsersModels
    {
        [Key]
        public string UserId { get; set; }
    }
}