using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
using DrawIt3.Models;
using DrawIt3.Migrations;

namespace DrawIt3.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string UsingRoom { get; set; }
        public string connection { get; set; }

        public virtual Room Rooms { get; set; }

        public int? currentRoomScore { get; set; }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext()
            : base("MyConnectionString")
        {
            System.Data.Entity.Database.SetInitializer(new ApplicationDbContextConfiguration());
        }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<RoomUser> RoomUsers { get; set; }

        public virtual IList<ApplicationUser> UsersInRoom { get; set; }
    }
}