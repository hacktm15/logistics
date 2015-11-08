using LogisticsAPI.Controllers;

namespace LogisticsAPI.Migrations
{
    using DataAccess;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LogisticsAPI.DataAccess.DBContextLogistics>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        public static void DemoSeed()
        {
            List<Location> locations = new List<Location>();
            List<Category> categories= new List<Category>();

            using (var db = new DBUnitOfWork())
            {
                try
                {
                    var alreadySeeded = db.Repository<Category>().Count() != 0;
                    if (alreadySeeded)
                        return;

                }
                catch (Exception)
                {
                    return;
                }
            }
            using (var db = new DBUnitOfWork())
            {
                try
                {
                    var alreadySeeded = db.Repository<Category>().Count() != 0;
                    if (alreadySeeded)
                        return;

                    db.Repository<Category>().Add(
                        new[]
                        {
                            new Category() { Name = "Birotică", Description = "Papetărie, Pixărie etc." },
                            new Category() { Name = "Mese", Description = "Mobila" },
                            new Category() { Name = "Scaune", Description = "Mobila" },
                            new Category() { Name = "Computere", Description = "Electronice" },
                            new Category() { Name = "Periferice", Description = "Electronice" },
                            new Category() { Name = "Monitoare", Description = "Electronice" },
                            new Category() { Name = "Cabluri", Description = "Conectica" },
                            new Category() { Name = "Mufe", Description = "Conectica" },
                        }
                    );

                    categories.AddRange(db.Repository<Category>().GetAll());

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            using (var db = new DBUnitOfWork())
            {

                try
                {
                    db.Repository<Location>().Add(
                        new[]
                        {
                            new Location { Name = @"Unknown", Description = @"The inevitable destination of ALL things" },
                            new Location { Name = @"Gardenzone", Description = @"A mythical location with a tower of rooms" },
                            new Location { Name = @"Gardenzone\Lăcaș", Description = @"A mythical room in a mythical location with a tower of rooms" },
                            new Location { Name = @"Gardenzone\Lăcaș\Dulap-1", Description = @"A 1st mythical locker in a mythical room in a mythical location with a tower of rooms" },
                            new Location { Name = @"Gardenzone\Lăcaș\Dulap-2", Description = @"A 2nd mythical locker in a mythical room in a mythical location with a tower of rooms" },
                            new Location { Name = @"Gardenzone\Lăcaș\Dulap-3", Description = @"A 3rd mythical locker in a mythical room in a mythical location with a tower of rooms" },
                            new Location { Name = @"Gardenzone\Lăcaș\Dulap-4", Description = @"A 4th mythical locker in a mythical room in a mythical location with a tower of rooms" },
                            new Location { Name = @"Gardenzone\Lăcaș\Dulap-5", Description = @"A 5th mythical locker in a mythical room in a mythical location with a tower of rooms" },
                            new Location { Name = @"Gardenzone\Lăcaș\Dulap-6", Description = @"A 6th mythical locker in a mythical room in a mythical location with a tower of rooms" },
                            new Location { Name = @"Aprozar", Description = @"A location where a lot of transactions take place" },
                            new Location { Name = @"Aprozar\Masa-1", Description = @"A 1st table from a location where a lot of transactions take place" },
                            new Location { Name = @"Aprozar\Masa-2", Description = @"A 2nd table from a location where a lot of transactions take place" },
                            new Location { Name = @"Aprozar\Masa-3", Description = @"A 3rd table from a location where a lot of transactions take place" },
                            new Location { Name = @"Aprozar\Masa-4", Description = @"A 4th table from a location where a lot of transactions take place" },
                            new Location { Name = @"Aprozar\Masa-5", Description = @"A 5th table from a location where a lot of transactions take place" },
                            new Location { Name = @"Aprozar\Masa-6", Description = @"A 6th table from a location where a lot of transactions take place" },
                            new Location { Name = @"Aprozar\Masa-7", Description = @"A 7th table from a location where a lot of transactions take place" },
                            new Location { Name = @"Aprozar\Masa-8", Description = @"A 8th table from a location where a lot of transactions take place" },
                            new Location { Name = @"Aprozar\Masa-9", Description = @"A 9th table from a location where a lot of transactions take place" },
                            new Location { Name = @"Aprozar\Masa-10", Description = @"A 10th table from a location where a lot of transactions take place" },
                            new Location { Name = @"Aprozar\Masa-11", Description = @"A 11th table from a location where a lot of transactions take place" },
                            new Location { Name = @"Aprozar\Masa-12", Description = @"A 12th table from a location where a lot of transactions take place" },
                            new Location { Name = @"Aprozar\Masa-13", Description = @"A 13th table from a location where a lot of transactions take place" },
                            new Location { Name = @"Aprozar\Masa-14", Description = @"A 14th table from a location where a lot of transactions take place" },
                            new Location { Name = @"Aprozar\Masa-15", Description = @"A 15th table from a location where a lot of transactions take place" },
                            new Location { Name = @"Aprozar\Birou", Description = @"A desk from a location where a lot of transactions take place" },
                            new Location { Name = @"Aprozar\Dulap-1", Description = @"A 1st locker from a location where a lot of transactions take place" },
                            new Location { Name = @"Aprozar\Dulap-2", Description = @"A 2nd locker from a location where a lot of transactions take place" },
                            new Location { Name = @"Bârlog", Description = @"Home, where your airlock is" },
                            new Location { Name = @"Bârlog\Birou-1", Description = @"A 1st desk from home, where your airlock is" },
                            new Location { Name = @"Bârlog\Birou-2", Description = @"A 2nd desk from home, where your airlock is" },
                            new Location { Name = @"Bârlog\Birou-3", Description = @"A 3rd desk from home, where your airlock is" },
                            new Location { Name = @"Bârlog\Dulap-1", Description = @"A 1st locker from home, where your airlock is" },
                            new Location { Name = @"Bârlog\Dulap-2", Description = @"A 2nd locker from home, where your airlock is" },
                            new Location { Name = @"Bârlog\Dulap-3", Description = @"A 3rd locker from home, where your airlock is" },
                            new Location { Name = @"Bârlog\Dulap-4", Description = @"A 4th locker from home, where your airlock is" },
                        }
                    );

                    locations.AddRange(db.Repository<Location>().GetAll());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            using (var db = new DBUnitOfWork())
            {
                try
                {
                    db.Repository<Item>().Add(
                        new Item[]
                        {
                            new Item {
                                Name = "Top A4", Description = @"The paper type you beg for before an exam",
                                Quantity = 7,  MinQuantity = 8, Price = 10.0, Relevance = 10,
                                Picture = @"http://cdn.clubafaceri.ro/clients/98/86756/95/top-hartie-a4-1393188_big.jpg",
                                Categories = new [] { categories.FindLast(X => X.Name == @"Birotică") },
                                LocationId = locations.FindLast(X => X.Name == @"Gardenzone\Lăcaș\Dulap-1").EntityId },
                            new Item {
                                Name = "Top A3", Description = @"The paper type you beg for us to cut in half before an exam",
                                Quantity = 12, MinQuantity = 10, Price = 20.0, Relevance = 10,
                                Picture = @"http://images6.okr.ro/serve/auctions.v7/2013/feb/25/5d135e31f3f0bb8f93ccfda7e9dcca55-2526590-300_300.jpg",
                                Categories = new [] { categories.FindLast(X => X.Name == @"Birotică") },
                                LocationId = locations.FindLast(X => X.Name == @"Gardenzone\Lăcaș\Dulap-2").EntityId },
                            new Item {
                                Name = "BUTStone Computer", Description = @"Old, but still useful",
                                Quantity = 15,  MinQuantity = 10, Price = 1000.0, Relevance = 4,
                                Picture = @"http://content.etilize.com/Front/1027903902.jpg",
                                Categories = new [] { categories.FindLast(X => X.Name == @"Computere") },
                                LocationId = locations.FindLast(X => X.Name == @"Aprozar").EntityId },
                            new Item {
                                Name = "Pârci Computer", Description = @"Made from a random asortment of stuff we found",
                                Quantity = 4, MinQuantity = 2, Price = 1100.0, Relevance = 3,
                                Picture = @"http://www.kabajunkmovers.com/wp-content/uploads/2013/02/moving-trash-removal-300x300.jpg",
                                Categories = new [] { categories.FindLast(X => X.Name == @"Computere") },
                                LocationId = locations.FindLast(X => X.Name == @"Bârlog").EntityId },
                            new Item {
                                Name = "Pix de Vară", Description = @"Pen, summer activity edition",
                                Quantity = 200, MinQuantity = 100, Price = 1.0, Relevance = 5,
                                Picture = @"http://www.xump.com/Images/Products/MazePen-300A.jpg",
                                Categories = new [] { categories.FindLast(X => X.Name == @"Birotică") },
                                LocationId = locations.FindLast(X => X.Name == @"Aprozar\Dulap-2").EntityId },
                            new Item {
                                Name = "Scaun Pliabil", Description = @"Made of lovely spinter-free wood",
                                Quantity = 15, MinQuantity = 30, Price = 120.0, Relevance = 1,
                                Picture = @"http://www.polyvore.com/cgi/img-thing?.out=jpg&size=l&tid=84061367",
                                Categories = new [] { categories.FindLast(X => X.Name == @"Scaune") },
                                LocationId = locations.FindLast(X => X.Name == @"Gardenzone").EntityId },
                            new Item {
                                Name = "Scaun Rabatabil", Description = @"Made to slide gracefully off the staircase",
                                Quantity = 5, MinQuantity = 30, Price = 90.0, Relevance = 1,
                                Picture = @"http://www.birotica-braila.ro/images/Mobilier%20de%20birou/170ScaunGolf%20X.jpg",
                                Categories = new [] { categories.FindLast(X => X.Name == @"Scaune") },
                                LocationId = locations.FindLast(X => X.Name == @"Bârlog").EntityId },
                            new Item {
                                Name = "Scaun Decapotabil", Description = @"Made with a pull-up seat; not to be confused with a toilet",
                                Quantity = 23, MinQuantity = 30, Price = 100.0, Relevance = 1,
                                Picture = @"http://www.romedic.ro/arata_img.php?img=http://www.euro-pc.ro/image/data/1314623588.jpg&w=300&h=300",
                                Categories = new [] { categories.FindLast(X => X.Name == @"Scaune") },
                                LocationId = locations.FindLast(X => X.Name == @"Aprozar").EntityId },
                            new Item {
                                Name = "CTRL+ALT+DEL Monitor Model Flexible", Description = @"You've seen it",
                                Quantity = 15, MinQuantity = 15, Price = 300.0, Relevance = 2,
                                Picture = @"http://ecx.images-amazon.com/images/I/41EPoJyS-HL._SY300_.jpg",
                                Categories = new [] { categories.FindLast(X => X.Name == @"Monitoare") },
                                LocationId = locations.FindLast(X => X.Name == @"Aprozar").EntityId },
                            new Item {
                                Name = "CTRL+ALT+DEL Monitor Model fRigid", Description = @"It's useful, but it can't bend overbackwards for you",
                                Quantity = 3, MinQuantity = 5, Price = 300.0, Relevance = 2,
                                Picture = @"http://content.etilize.com/300/1010741302.jpg",
                                Categories = new [] { categories.FindLast(X => X.Name == @"Monitoare") },
                                LocationId = locations.FindLast(X => X.Name == @"Bârlog").EntityId },
                            new Item {
                                Name = "6MENS Monitor Model White", Description = @"Professional looking, but older than you",
                                Quantity = 3, MinQuantity = 0, Price = 300.0, Relevance = 2,
                                Picture = @"http://com2000.hr/wp-content/uploads/2014/08/FSC-B17-5.jpg",
                                Categories = new [] { categories.FindLast(X => X.Name == @"Monitoare") },
                                LocationId = locations.FindLast(X => X.Name == @"Bârlog").EntityId }
                        }
                    );

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            using (var db = new DBUnitOfWork())
            {
                try
                {
                    foreach (var item in db.Repository<Item>().GetAll())
                    {
                        WarningController.UpdateWarningForItem(item);
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        protected override void Seed(LogisticsAPI.DataAccess.DBContextLogistics context)
        {
            DemoSeed();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //context.Person.AddOrUpdate(
            //    p => p.FullName,
            //    new Person { FullName = "Andrew Peters" },
            //    new Person { FullName = "Brice Lambson" },
            //    new Person { FullName = "Rowan Miller" }
            //);
        }
    }
}
