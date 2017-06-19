using DrawIt3.Models;


namespace DrawIt3.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    class ApplicationDbContextConfiguration: System.Data.Entity.MigrateDatabaseToLatestVersion<ApplicationDbContext, MigrtationConfiguration>
    {

    }
    internal sealed class MigrtationConfiguration : DbMigrationsConfiguration<DrawIt3.Models.ApplicationDbContext>
    {
        public MigrtationConfiguration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DrawIt3.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.Words.AddOrUpdate(
              x => x.Id,
              new Word { word = "Cat" },
              new Word { word = "Ghost" },
              new Word { word = "Cow" },
              new Word { word = "Bug" },

              new Word { word = "Snake" },
              new Word { word = "Lips" },
              new Word { word = "Socks" },
              new Word { word = "Coat" },
              new Word { word = "Heart" },
              new Word { word = "Kite" },
              new Word { word = "Milk" },
              new Word { word = "Skateboard" },
              new Word { word = "Apple" },
              new Word { word = "Mouse" },
              new Word { word = "Star" },
              new Word { word = "Whale" },
              new Word { word = "Hippo" },
              new Word { word = "Face" },
              new Word { word = "Ice Cream" },
              new Word { word = "Spoon" },
              new Word { word = "Sun" },
              new Word { word = "Flower" },
              new Word { word = "Banana" },
              new Word { word = "Book" },
              new Word { word = "Light" },
              new Word { word = "Apple" },
              new Word { word = "Smile" },
              new Word { word = "Shoe" },
              new Word { word = "Hat" },
              new Word { word = "Dog" },
              new Word { word = "Duck" },
              new Word { word = "Bird" },
              new Word { word = "Person" },
              new Word { word = "Ball" },
              new Word { word = "Nose" },
              new Word { word = "Jacket" },
              new Word { word = "Beach" },
              new Word { word = "Cookie" },
              new Word { word = "Drum" },
              new Word { word = "Worm" },
              new Word { word = "Cup" },
              new Word { word = "Pie" },
              new Word { word = "Jar" },
              new Word { word = "Tree" },
              new Word { word = "Slide" },
              new Word { word = "Swing" },
              new Word { word = "Water" },

              new Word { word = "Ocean" }



            );

        }
    }
}
