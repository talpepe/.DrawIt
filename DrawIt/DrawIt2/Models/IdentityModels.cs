using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DrawIt.Models
{
    public class IdentityModels
    {
    }

    public class ApplicationUser : IdentityUser
    {
        public string UsingRoom { get; set; }
        public string connection { get; set; }

        public virtual RoomModels RoomModels { get; set; } 
    
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext() : base ("MyConnectionString")
        {

        }

        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<RoomModels> Rooms { get; set; }
        public DbSet<RoomUsersModels> RoomUsers { get; set; }

        public virtual ICollection<ApplicationUser> UsersInRoom { get; set; }
    }
}