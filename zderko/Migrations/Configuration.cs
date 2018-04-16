namespace zderko.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using zderko.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<zderko.Models.ZderkoDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(zderko.Models.ZderkoDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.


            IEnumerable<MethodOfPayment> methodsOfPayment = new List<MethodOfPayment>()
                {
                new MethodOfPayment(){
                    ID = 1,
                    Name = "Gotovina",
                    Value = 1
                },
                new MethodOfPayment(){
                    ID = 2,
                    Name = "Kartica",
                    Value = 2
                }
            };

            context.MethodsOfPayment.AddRange(methodsOfPayment);

            if (!context.Roles.Any(r => r.Name == "USER"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "USER" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "ADMIN"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "ADMIN" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "RESTAURANT_MANAGER"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "RESTAURANT_MANAGER" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "admin@gmail.com"))
            {
                var store = new UserStore<User>(context);
                var manager = new UserManager<User>(store);
                var user = new User {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com"
                };

                manager.Create(user, "ChangeItAsap!");
                manager.AddToRole(user.Id, "ADMIN");
            }

            if (!context.Users.Any(u => u.UserName == "user@gmail.com"))
            {
                var store = new UserStore<User>(context);
                var manager = new UserManager<User>(store);
                var user = new User {
                    UserName = "user@gmail.com",
                    Email = "user@gmail.com"
                };

                manager.Create(user, "ChangeItAsap!");
                manager.AddToRole(user.Id, "USER");
            }

            if (!context.Users.Any(u => u.UserName == "restaurantManager@gmail.com"))
            {
                var store = new UserStore<User>(context);
                var manager = new UserManager<User>(store);
                var user = new User {
                    UserName = "restaurantManager@gmail.com",
                    Email = "restaurantManager@gmail.com"
                };

                manager.Create(user, "ChangeItAsap!");
                manager.AddToRole(user.Id, "RESTAURANT_MANAGER");
            }

            IEnumerable<Restaurant> restaurants = new List<Restaurant>()
                {
                new Restaurant(){
                    ID = 1,
                    Name = "Konoba Didov San",
                    EmailAddress= "didov.san@gmail.com",
                    PhoneNumber = 016248283,
                    User = context.Users.ToList().SingleOrDefault(u => u.UserName == "restaurantManager@gmail.com"),
                    Address = new Address()
                    {
                      City = "Zagreb",
                      Street = "Mletaèka",
                      PostalCode = 10000,
                      Country = "Croatia",
                      HouseNumber = "11",
                      Name = "Adresa restorana"                                             
                    },
                    HourOfDayFrom = 11,
                    HourOfDayTo = 22
                },
                new Restaurant(){
                    ID = 2,
                    Name = "Submarine burgers",
                    EmailAddress= "submarine@gmail.com",
                    PhoneNumber = 016233433,
                    User = context.Users.ToList().SingleOrDefault(u => u.UserName == "restaurantManager@gmail.com"),
                    Address = new Address()
                    {
                      City = "Zagreb",
                      Street = "Radnièka",
                      PostalCode = 10000,
                      Country = "Croatia",
                      HouseNumber = "34",
                      Name = "Adresa restorana"
                    },
                    HourOfDayFrom = 9,
                    HourOfDayTo = 23
                }
            };

            context.Restaurants.AddRange(restaurants);

            IEnumerable<Dish> dishes = new List<Dish>()
                {
                new Dish(){
                    ID = 1,
                    Name = "Jagnjetina pod pekon",
                    Price = 350,
                    RestaurantId = 1,
                    Description = "Osjetite okus peke iz dalmatinske zagore."
                },
                 new Dish(){
                    ID = 2,
                    Name = "Babina tava s prilogom",
                    Price = 80,
                    RestaurantId = 1,
                    Description = "Onako kako to babe iz dalmatinske zagore prave."
                },
                  new Dish(){
                    ID = 3,
                    Name = "Smokehouse burger",
                    Price = 55,
                    RestaurantId = 2,
                    Description = "Burger s dimljenom govedinom."
                },
                   new Dish(){
                    ID = 4,
                    Name = "Original burger",
                    Price = 54,
                    RestaurantId = 2,
                    Description = "Burger po originalnoj recepturi."
                }
            };

            context.Dishes.AddRange(dishes);
        }
    }
}
