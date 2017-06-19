/*using DrawIt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DrawIt.Context
{
    public class AppContext : DbContext
    {
        public AppContext()
            : base("name=MyConnectionString")
        {
        }
        public DbSet<RoomModels> Rooms { get; set; }

        public virtual ICollection<ApplicationUser> UsersInRoom { get; set; }
    }
}*/