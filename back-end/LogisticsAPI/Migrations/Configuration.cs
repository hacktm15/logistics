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
            Location DefaultLocation = null;

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
                            new Category() { Name = "Papetarie", Description = "Birotica" },
                            new Category() { Name = "Mese", Description = "Mobila" },
                            new Category() { Name = "Scaune", Description = "Mobila" },
                            new Category() { Name = "Computere", Description = "Electronice" },
                            new Category() { Name = "Periferice", Description = "Electronice" },
                            new Category() { Name = "Monitoare", Description = "Electronice" },
                            new Category() { Name = "Cabluri", Description = "Conectica" },
                            new Category() { Name = "Mufe", Description = "Conectica" },
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
                    db.Repository<Location>().Add(
                        new[]
                        {
                            new Location { Name = @"Unknown" },
                            new Location { Name = @"Gardenzone" },
                            new Location { Name = @"Gardenzone\Lăcaș" },
                            new Location { Name = @"Gardenzone\Lăcaș\Dulap-1" },
                            new Location { Name = @"Gardenzone\Lăcaș\Dulap-2" },
                            new Location { Name = @"Gardenzone\Lăcaș\Dulap-3" },
                            new Location { Name = @"Gardenzone\Lăcaș\Dulap-4" },
                            new Location { Name = @"Gardenzone\Lăcaș\Dulap-5" },
                            new Location { Name = @"Gardenzone\Lăcaș\Dulap-6" },
                            new Location { Name = @"Aprozar" },
                            new Location { Name = @"Aprozar\Masa-1" },
                            new Location { Name = @"Aprozar\Masa-2" },
                            new Location { Name = @"Aprozar\Masa-3" },
                            new Location { Name = @"Aprozar\Masa-4" },
                            new Location { Name = @"Aprozar\Masa-5" },
                            new Location { Name = @"Aprozar\Masa-6" },
                            new Location { Name = @"Aprozar\Masa-7" },
                            new Location { Name = @"Aprozar\Masa-8" },
                            new Location { Name = @"Aprozar\Masa-9" },
                            new Location { Name = @"Aprozar\Masa-10" },
                            new Location { Name = @"Aprozar\Masa-11" },
                            new Location { Name = @"Aprozar\Masa-12" },
                            new Location { Name = @"Aprozar\Masa-13" },
                            new Location { Name = @"Aprozar\Masa-14" },
                            new Location { Name = @"Aprozar\Masa-15" },
                            new Location { Name = @"Aprozar\Birou" },
                            new Location { Name = @"Aprozar\Dulap-1" },
                            new Location { Name = @"Aprozar\Dulap-2" },
                            new Location { Name = @"Bârlog" },
                            new Location { Name = @"Bârlog\Birou-1" },
                            new Location { Name = @"Bârlog\Birou-2" },
                            new Location { Name = @"Bârlog\Birou-3" },
                            new Location { Name = @"Bârlog\Dulap-1" },
                            new Location { Name = @"Bârlog\Dulap-2" },
                            new Location { Name = @"Bârlog\Dulap-3" },
                            new Location { Name = @"Bârlog\Dulap-4" },
                        }
                    );

                    DefaultLocation = db.Repository<Location>().GetAll().FirstOrDefault(X => X.Name == "Unknown");
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
                        new[]
                        {
                            new Item { Name = "Top A4", Quantity = 7,  MinQuantity = 8, Price = 10.0, LocationId = DefaultLocation.EntityId },
                            new Item { Name = "Top A3", Quantity = 12, Price = 20.0, LocationId = DefaultLocation.EntityId },
                            new Item { Name = "BUTTStone Computer", Quantity = 15,  MinQuantity = 10, Price = 1000.0, LocationId = DefaultLocation.EntityId },
                            new Item { Name = "Pârci Computer", Quantity = 4, Price = 1100.0, LocationId = DefaultLocation.EntityId },
                            new Item { Name = "Pix de Vară", Quantity = 200, Price = 1.0, LocationId = DefaultLocation.EntityId },
                            new Item { Name = "Scaun Decapotabil", Quantity = 23, MinQuantity = 30, Price = 90.0, LocationId = DefaultLocation.EntityId },
                            new Item { Name = "CTRL+ALT+DEL Monitor Model Flexibil", Quantity = 15, Price = 300.0, LocationId = DefaultLocation.EntityId },
                            new Item { Name = "CTRL+ALT+DEL Monitor Model fRigid", Quantity = 3,  MinQuantity = 5,  Price = 300.0, LocationId = DefaultLocation.EntityId },
                            new Item { Name = "6MENS Monitor Model White", Quantity = 3, Price = 300.0, LocationId = DefaultLocation.EntityId },
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

        public static void DemoSeedBlock()
        {
            using (var db = new DBUnitOfWork())
            {
                try
                {
                    db.Repository<Category>().Add(new Category() { Name = "Papetarie", Description = "Birotica" });
                    db.Repository<Category>().Add(new Category() { Name = "Mese", Description = "Mobila" });
                    db.Repository<Category>().Add(new Category() { Name = "Scaune", Description = "Mobila" });
                    db.Repository<Category>().Add(new Category() { Name = "Computere", Description = "Electronice" });
                    db.Repository<Category>().Add(new Category() { Name = "Periferice", Description = "Electronice" });
                    db.Repository<Category>().Add(new Category() { Name = "Monitoare", Description = "Electronice" });
                    db.Repository<Category>().Add(new Category() { Name = "Cabluri", Description = "Conectica" });
                    db.Repository<Category>().Add(new Category() { Name = "Mufe", Description = "Conectica" });
/*
                    db.Repository<Location>().Add(
                        new[]
                        {
                            new Location { Name = @"Gardenzone" },
                            new Location { Name = @"Gardenzone\Lăcaș" },
                            new Location { Name = @"Gardenzone\Lăcaș\Dulap-1" },
                            new Location { Name = @"Gardenzone\Lăcaș\Dulap-2" },
                            new Location { Name = @"Gardenzone\Lăcaș\Dulap-3" },
                            new Location { Name = @"Gardenzone\Lăcaș\Dulap-4" },
                            new Location { Name = @"Gardenzone\Lăcaș\Dulap-5" },
                            new Location { Name = @"Gardenzone\Lăcaș\Dulap-6" },
                            new Location { Name = @"Aprozar" },
                            new Location { Name = @"Aprozar\Masa-1" },
                            new Location { Name = @"Aprozar\Masa-2" },
                            new Location { Name = @"Aprozar\Masa-3" },
                            new Location { Name = @"Aprozar\Masa-4" },
                            new Location { Name = @"Aprozar\Masa-5" },
                            new Location { Name = @"Aprozar\Masa-6" },
                            new Location { Name = @"Aprozar\Masa-7" },
                            new Location { Name = @"Aprozar\Masa-8" },
                            new Location { Name = @"Aprozar\Masa-9" },
                            new Location { Name = @"Aprozar\Masa-10" },
                            new Location { Name = @"Aprozar\Masa-11" },
                            new Location { Name = @"Aprozar\Masa-12" },
                            new Location { Name = @"Aprozar\Masa-13" },
                            new Location { Name = @"Aprozar\Masa-14" },
                            new Location { Name = @"Aprozar\Masa-15" },
                            new Location { Name = @"Aprozar\Birou" },
                            new Location { Name = @"Aprozar\Dulap-1" },
                            new Location { Name = @"Aprozar\Dulap-2" },
                            new Location { Name = @"Bârlog" },
                            new Location { Name = @"Bârlog\Birou-1" },
                            new Location { Name = @"Bârlog\Birou-2" },
                            new Location { Name = @"Bârlog\Birou-3" },
                            new Location { Name = @"Bârlog\Dulap-1" },
                            new Location { Name = @"Bârlog\Dulap-2" },
                            new Location { Name = @"Bârlog\Dulap-3" },
                            new Location { Name = @"Bârlog\Dulap-4" },
                        }
                    );

                    db.Repository<Item>().Add(
                        new[]
                        {
                            new Item { Name = "Top A4", Quantity = 7, Price = 10.0 },
                            new Item { Name = "Top A3", Quantity = 12, Price = 20.0 },
                            new Item { Name = "BUTTStone Computer", Quantity = 15, Price = 1000.0 },
                            new Item { Name = "Pârci Computer", Quantity = 4, Price = 1100.0 },
                            new Item { Name = "Pix de Vară", Quantity = 200, Price = 1.0 },
                            new Item { Name = "Scaun Decapotabil", Quantity = 23, Price = 90.0 },
                            new Item { Name = "CTRL+ALT+DEL Monitor Model Flexibil", Quantity = 15, Price = 300.0 },
                            new Item { Name = "CTRL+ALT+DEL Monitor Model fRigid", Quantity = 3, Price = 300.0 },
                            new Item { Name = "6MENS Monitor Model White", Quantity = 3, Price = 300.0 },
                        }
                    );
                    */
                }
                catch (Exception ex)
                {
                    throw ex;
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
