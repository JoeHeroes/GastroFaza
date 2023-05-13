using GastroFaza.Models;
using GastroFaza.Models.Enum;

namespace GastroFaza.Test
{
    public class DataTestDBInitializer
    {
        public DataTestDBInitializer()
        {
        }

        public void Seed(RestaurantDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Addresses.AddRange(
                new Address() { City = "1", Street = "1", PostalCode = "1" },
                new Address() { City = "2", Street = "2", PostalCode = "2" },
                new Address() { City = "3", Street = "3", PostalCode = "3" }

            );

            context.Clients.AddRange(
                new Client()
                {
                    Email = "test1@wp.pl",
                    FirstName = "test1",
                    LastName = "test1",
                    DateOfBirth = new DateTime(),
                    Nationality = "Poland",
                },
                new Client()
                {
                    Email = "test2@wp.pl",
                    FirstName = "test2",
                    LastName = "test2",
                    DateOfBirth = new DateTime(),
                    Nationality = "Poland",
                },
                new Client()
                {
                    Email = "test3@wp.pl",
                    FirstName = "test3",
                    LastName = "test3",
                    DateOfBirth = new DateTime(),
                    Nationality = "Poland",
                }
                );

            context.Tables.AddRange(
                new DiningTable()
                {
                    Busy = false,
                    Seats = 1
                },
                new DiningTable()
                {
                    Busy = false,
                    Seats = 2
                },
                new DiningTable()
                {
                    Busy = false,
                    Seats = 3
                },
                new DiningTable()
                {
                    Busy = false,
                    Seats = 4
                }
           );
            

            context.Dishs.AddRange(
               new Dish()
               {
                   Name = "Test1",
                   Description = "Test1",
                   Price = 15.5,
                   DishType = DishType.Pizza,
                   ProfileImg = "Test/Test1.png"
               },
               new Dish()
               {
                   Name = "Test2",
                   Description = "Test2",
                   Price = 15.5,
                   DishType = DishType.Pizza,
                   ProfileImg = "Test/Test2.png"
               },
               new Dish()
               {
                   Name = "Test3",
                   Description = "Test3",
                   Price = 15.5,
                   DishType = DishType.Pizza,
                   ProfileImg = "Test/Test3.png"
               }
           );

            context.Orders.AddRange(
               new Order()
               {
                   Status = Status.Nowe,
                   Description = "Test",
                   Price = 1.1,
                   AddedById = 1
               }
            );
            context.SaveChanges();

            context.DishOrders.AddRange(
                new DishOrder()
                {
                    DishesId = 1,
                    OrderId = 1
                }
            );


            context.Restaurants.AddRange(
                new Restaurant()
                {
                    Name = "Test",
                    Description = "TestTestTest",
                    HasDelivery = true,
                    ContactEmail = "Test@wp.pl",
                    ContactNumber = "123 456 789",
                    Address = new Address()
                    {
                        City = "Test",
                        PostalCode = "12-234",
                        Street = "Test"
                    }
                },
                new Restaurant()
                {
                    Name = "Test2",
                    Description = "TestTestTest2",
                    HasDelivery = true,
                    ContactEmail = "Test2@wp.pl",
                    ContactNumber = "123 456 789",
                    Address = new Address()
                    {
                        City = "Test2",
                        PostalCode = "12-234",
                        Street = "Test2"
                    },
                });

            context.Roles.AddRange(
                new Role()
                {
                    Name = "Waiter"
                },
                 new Role()
                 {
                     Name = "Cook"
                 },
                 new Role()
                 {
                     Name = "Manager"
                 });

            context.Workers.AddRange(
                new Worker()
                {
                    Email = "Test@wp.pl",
                    FirstName = "Test",
                    LastName = "Test",
                    DateOfBirth = new DateTime(),
                    Nationality = "Test",
                    RoleId = 3,
                },
                new Worker()
                {
                    Email = "Test2@wp.pl",
                    FirstName = "Tes2",
                    LastName = "Test2",
                    DateOfBirth = new DateTime(),
                    Nationality = "Test2",
                    RoleId = 3,
                });



            context.SaveChanges();
        }
    }
}

