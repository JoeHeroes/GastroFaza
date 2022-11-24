using GastroFaza.Models;
using GastroFaza.Models.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GastroFaza.Seeder
{
    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext dbContext;

        public RestaurantSeeder(RestaurantDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Seed()
        {
            if (dbContext.Database.CanConnect())
            {
                var pending = this.dbContext.Database.GetPendingMigrations();
                if(pending != null && pending.Any())
                {
                    this.dbContext.Database.Migrate();
                }

                if (!dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    dbContext.Roles.AddRange(roles);
                    dbContext.SaveChanges();
                }

                if (!dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    dbContext.Restaurants.AddRange(restaurants);
                    dbContext.SaveChanges();
                }

                if (!dbContext.Clients.Any())
                {
                    var clients = GetClients();
                    dbContext.Clients.AddRange(clients);
                    dbContext.SaveChanges();
                }

                if (!dbContext.Workers.Any())
                {
                    var workers = GetWorkers();
                    dbContext.Workers.AddRange(workers);
                    dbContext.SaveChanges();
                }
                
                if (!dbContext.Dishs.Any())
                {
                    var dishes = GetDishes();
                    dbContext.Dishs.AddRange(dishes);
                    dbContext.SaveChanges();
                }

                if (!dbContext.Addresses.Any())
                {
                    var addresses = GetAddresses();
                    dbContext.Addresses.AddRange(addresses);
                    dbContext.SaveChanges();
                }

                if (!dbContext.Orders.Any())
                {
                    var orders = GetOrders();
                    dbContext.Orders.AddRange(orders);
                    dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Order> GetOrders()
        {
            return new List<Order>()
            {
                new Order()
                {

                }
            };
        }

        private IEnumerable<Address> GetAddresses()
        {
            return new List<Address>()
            {
                new Address()
                {
                      
                }
            };
        }

        private IEnumerable<Dish> GetDishes()
        {
            return new List<Dish>()
            {
                new Dish()
                {

                }
                
            };
        }

        private IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client()
                {
                     Email = "JoeHeros@wp.pl",
                     FirstName = "Joe",
                     LastName = "Heros",
                     DateOfBirth = new DateTime(),
                     Nationality = "Poland",
                }
            };
        }

        private IEnumerable<Worker> GetWorkers()
        {
            return new List<Worker>()
            {
                new Worker()
                {
                     Email = "Krzys@wp.pl",
                     FirstName = "Krzys",
                     LastName = "Lulaj",
                     DateOfBirth = new DateTime(),
                     Nationality = "UK",
                     RoleId = 3,
                }
            };
        }

        private IEnumerable<Role> GetRoles()
        {
            return new List<Role>()
            {
                new Role()
                {
                    Name ="Kelner"
                },
                new Role()
                {
                    Name ="Kucharz"
                },
                new Role()
                {
                    Name ="Menadżer"
                },
            };
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            return new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name="GastroFaza",
                    Description="Restaurant GastroFaza",
                    HasDelivery=false,
                    ContactEmail="GastroFaza.Contact@wp.pl",
                    ContactNumber="503 345 412",
                    Address = new Address()
                    {
                        City = "Białystok",
                        Street = "Piekna 7",
                        PostalCode = "30-032"
                    },
                    Menu = new List<Dish>
                    {
                        new Dish() 
                        {
                            Name = "Margarita",
                            Description = "Margarita",
                            Price = 15.5,
                            DishType = DishType.Pizza,
                        },
                        new Dish() 
                        {
                            Name = "Pasta with Tomato",
                            Description = "Tomato",
                            Price = 24.5,
                            DishType = DishType.Pasta,
                        },
                        new Dish() 
                        {
                            Name = "Pasta with Tomato",
                            Description = "Tomato",
                            Price = 24.5,
                            DishType = DishType.Pasta,
                        },
                        new Dish()
                        {
                            Name = "ChesseBurger",
                            Description = "Burger",
                            Price = 29,
                            DishType = DishType.Burger,
                        },
                        new Dish()
                        {
                            Name = "Burger Kimchi",
                            Description = "Burger",
                            Price = 36,
                            DishType = DishType.Burger,
                        },
                        new Dish()
                        {
                            Name = "Cezar",
                            Description = "Sałatka",
                            Price = 33,
                            DishType= DishType.Salad,
                        },
                        new Dish()
                        {
                            Name = "Leczo",
                            Description = "Makaron",
                            Price = 34,
                            DishType= DishType.Pasta,
                        },
                        new Dish()
                        {
                            Name = "Tom Yum",
                            Description = "Zupa",
                            Price = 29,
                            DishType= DishType.Soup,
                        },
                        new Dish()
                        {
                            Name = "Pad-Thai z krewetkami",
                            Description = "Makaron",
                            Price = 55,
                            DishType= DishType.Pasta,
                        },
                        new Dish()
                        {
                            Name = "Ramen",
                            Description = "Makaron",
                            Price = 37,
                            DishType= DishType.Pasta,
                        },
                        new Dish()
                        {
                            Name = "Lava Cake",
                            Description = "Deser",
                            Price = 22,
                            DishType= DishType.Dessert,
                        },
                        new Dish()
                        {
                            Name = "Old Fashioned",
                            Description = "Drink",
                            Price = 26,
                            DishType= DishType.Alcohol,
                        },
                        new Dish()
                        {
                            Name = "Aperol Spritz",
                            Description = "Drink",
                            Price = 27,
                            DishType= DishType.Alcohol,
                        },
                        new Dish()
                        {
                            Name = "Cuba Libre",
                            Description = "Drink",
                            Price = 23,
                            DishType= DishType.Alcohol,
                        },
                        new Dish()
                        {
                            Name = "Negroni",
                            Description = "Drink",
                            Price = 27,
                            DishType= DishType.Alcohol,
                        },
                        new Dish()
                        {
                            Name = "Coca Cola",
                            Description = "Napój",
                            Price = 10,
                            DishType= DishType.Drinks,
                        },
                        new Dish()
                        {
                            Name = "Fanta",
                            Description = "Napój",
                            Price = 10,
                            DishType= DishType.Drinks,
                        },
                        new Dish()
                        {
                            Name = "Cappy",
                            Description = "Napój",
                            Price = 8,
                            DishType= DishType.Drinks,
                        },
                    },
                    Workers = new List<Worker>
                    { 
                        new Worker()
                        {
                            Email = "krzys@wp.pl",
                            FirstName = "Krzysztof",
                            LastName = "Góral",
                            DateOfBirth = DateTime.Today,
                            RoleId = 1,
                            Rating = 5,
                            PasswordHash = "123"
                        },
                         new Worker()
                        {
                            Email = "Luki@wp.pl",
                            FirstName = "Łukasz",
                            LastName = "Chad",
                            DateOfBirth = DateTime.Today,
                            RoleId = 2,
                            Rating = 5,
                            PasswordHash = "123"
                        },
                        new Worker()
                        {
                            Email = "kornisz@wp.pl",
                            FirstName = "Kornel",
                            LastName = "Gołebiewski",
                            DateOfBirth = DateTime.Today,
                            RoleId = 3,
                            Rating = 5,
                            PasswordHash = "123"
                        },
                        new Worker()
                        {
                            Email = "finGermayass@wp.pl",
                            FirstName = "Fin",
                            LastName = "Germayass",
                            DateOfBirth = DateTime.Today,
                            RoleId = 1,
                            Rating = 5,
                            PasswordHash = "123"
                        },
                        new Worker()
                        {
                            Email = "peteOphile@wp.pl",
                            FirstName = "Pete",
                            LastName = "Ophile",
                            DateOfBirth = DateTime.Today,
                            RoleId = 1,
                            Rating = 5,
                            PasswordHash = "123"
                        }
                    },
                    Clients = new List<Client>()
                    {
                        new Client()
                        {
                            Email = "SlepyMichalek@wp.pl",
                            FirstName = "Slepy",
                            LastName = "Michalek",
                            DateOfBirth = DateTime.Today,
                            PasswordHash = "123"
                        },
                        new Client()
                        {
                            Email = "GrubyPaweł@wp.pl",
                            FirstName = "Gruby",
                            LastName = "Paweł",
                            DateOfBirth = DateTime.Today,
                            PasswordHash = "123"
                        },
                        new Client()
                        {
                            Email = "KingDavid@wp.pl",
                            FirstName = "David",
                            LastName = "King",
                            DateOfBirth = DateTime.Today,
                            PasswordHash = "123"
                        }
                    },
                    Tables = new List<Tablee>()
                    {
                        new Tablee()
                        {
                            Busy = true,
                            Reserved = false,
                            Seats = 8,
                            ClientId = 2
                        },
                        new Tablee()
                        {
                            Busy = true,
                            Reserved = false,
                            Seats = 8,
                            ClientId = 2
                        },
                        new Tablee()
                        {
                            Busy = true,
                            Reserved = false,
                            Seats = 12,
                            ClientId = 2
                        },
                        new Tablee()
                        {
                            Busy = true,
                            Reserved = false,
                            Seats = 4,
                            ClientId = 2
                        },
                        new Tablee()
                        {
                            Busy = true,
                            Reserved = false,
                            Seats = 4,
                            ClientId = 2
                        },
                        new Tablee()
                        {
                            Busy = true,
                            Reserved = false,
                            Seats = 4,
                            ClientId = 3
                        },
                        new Tablee()
                        {
                            Busy = false,
                            Reserved = false,
                            Seats = 2,
                            ClientId = 3
                        },
                        new Tablee()
                        {
                            Busy = false,
                            Reserved = true,
                            Seats = 2,
                            ClientId = 3
                        },
                        new Tablee()
                        {
                            Busy = false,
                            Reserved = false,
                            Seats = 2,
                            ClientId = 3
                        },
                        new Tablee()
                        {
                            Busy = false,
                            Reserved = true,
                            Seats = 2,
                            ClientId = 3
                        }
                    }
                },


                new Restaurant()
                {
                    Name="Ósemka",
                    Description="Restaurant Ósemka",
                    HasDelivery=false,
                    ContactEmail="Ósemka.Contact@wp.pl",
                    ContactNumber="213 713 372",
                    Address = new Address()
                    {
                        City = "Sztabin",
                        Street = "Augustowska 41",
                        PostalCode = "16-310"
                    },
                    Menu = new List<Dish>
                    {
                        new Dish()
                        {
                            Name = "Carpaccio z polędwicy wołowej",
                            Description = "Carpaccio",
                            Price = 42,
                            DishType = DishType.Pasta,
                        },
                        new Dish()
                        {
                            Name = "Tatar",
                            Description = "Tatar",
                            Price = 52,
                            DishType = DishType.Meat,
                        },
                        new Dish()
                        {
                            Name = "Rosół",
                            Description = "Rosół",
                            Price = 19,
                            DishType = DishType.Soup,
                        },
                        new Dish()
                        {
                            Name = "Sałatka cezar z łososiem",
                            Description = "sałatka",
                            Price = 46,
                            DishType = DishType.Salad,
                        },
                        new Dish()
                        {
                            Name = "Blackened Chicken",
                            Description = "Kurczak",
                            Price = 42,
                            DishType = DishType.Meat,
                        },
                        new Dish()
                        {
                            Name = "Żeberka BBQ",
                            Description = "Żeberka",
                            Price = 47,
                            DishType= DishType.Meat,
                        },
                        new Dish()
                        {
                            Name = "Polędwiczka wieprzowa",
                            Description = "Polędwiczka",
                            Price = 47,
                            DishType= DishType.Meat,
                        },
                        new Dish()
                        {
                            Name = "Tarta z jabłkami",
                            Description = "Tarta",
                            Price = 30,
                            DishType= DishType.Dessert,
                        },
                        new Dish()
                        {
                            Name = "Malinowa chmurka",
                            Description = "Ciasto z malinami",
                            Price = 21,
                            DishType= DishType.Dessert,
                        },
                        new Dish()
                        {
                            Name = "Spaghetti",
                            Description = "Makaron",
                            Price = 25,
                            DishType= DishType.Pasta,
                        },
                        new Dish()
                        {
                            Name = "Sernik",
                            Description = "Sernik",
                            Price = 19,
                            DishType= DishType.Dessert,
                        },
                        new Dish()
                        {
                            Name = "Kozel",
                            Description = "Drink",
                            Price = 10,
                            DishType= DishType.Alcohol,
                        },
                        new Dish()
                        {
                            Name = "Książęce",
                            Description = "Drink",
                            Price = 9,
                            DishType= DishType.Alcohol,
                        },
                        new Dish()
                        {
                            Name = "Jagermeister",
                            Description = "Drink",
                            Price = 13,
                            DishType= DishType.Alcohol,
                        },
                        new Dish()
                        {
                            Name = "Jack Daniels (burbon)",
                            Description = "Drink",
                            Price = 16,
                            DishType= DishType.Alcohol,
                        },
                        new Dish()
                        {
                            Name = "Coca Cola",
                            Description = "Napój",
                            Price = 10,
                            DishType= DishType.Drinks,
                        },
                        new Dish()
                        {
                            Name = "Fanta",
                            Description = "Napój",
                            Price = 10,
                            DishType= DishType.Drinks,
                        },
                        new Dish()
                        {
                            Name = "Cappy",
                            Description = "Napój",
                            Price = 8,
                            DishType= DishType.Drinks,
                        },
                    },
                    Workers = new List<Worker>
                    {
                        new Worker()
                        {
                            Email = "mikeOxlong@wp.pl",
                            FirstName = "Mike",
                            LastName = "Oxlong",
                            DateOfBirth = DateTime.Today,
                            RoleId = 3,
                            Rating = 5,
                            PasswordHash = "123"
                        },
                         new Worker()
                        {
                            Email = "adolfGazprom@wp.pl",
                            FirstName = "Adolf",
                            LastName = "Gazprom",
                            DateOfBirth = DateTime.Today,
                            RoleId = 2,
                            Rating = 5,
                            PasswordHash = "123"
                        },
                        new Worker()
                        {
                            Email = "isacNuts@wp.pl",
                            FirstName = "Isac",
                            LastName = "Nuts",
                            DateOfBirth = DateTime.Today,
                            RoleId = 3,
                            Rating = 5,
                            PasswordHash = "123"
                        },
                        new Worker()
                        {
                            Email = "johnBilon@wp.pl",
                            FirstName = "Jhon",
                            LastName = "Bilon",
                            DateOfBirth = DateTime.Today,
                            RoleId = 2,
                            Rating = 5,
                            PasswordHash = "123"
                        },
                        new Worker()
                        {
                            Email = "suzieGebels@wp.pl",
                            FirstName = "Suzie",
                            LastName = "Gebels",
                            DateOfBirth = DateTime.Today,
                            RoleId = 3,
                            Rating = 5,
                            PasswordHash = "123"
                        }
                    },
                    Clients = new List<Client>()
                    {
                        new Client()
                        {
                            Email = "HornyOrny@wp.pl",
                            FirstName = "Horny",
                            LastName = "Orny",
                            DateOfBirth = DateTime.Today,
                            PasswordHash = "123"
                        },
                        new Client()
                        {
                            Email = "PawełG@wp.pl",
                            FirstName = "Paweł",
                            LastName = "Górniak",
                            DateOfBirth = DateTime.Today,
                            PasswordHash = "123"
                        },
                        new Client()
                        {
                            Email = "GawełG@wp.pl",
                            FirstName = "Gaweł",
                            LastName = "Górniak",
                            DateOfBirth = DateTime.Today,
                            PasswordHash = "123"
                        },
                    },
                    Tables = new List<Tablee>()
                    {
                        new Tablee()
                        {
                            Busy = true,
                            Reserved = false,
                            Seats = 8,
                            ClientId = 2
                        },
                        new Tablee()
                        {
                            Busy = true,
                            Reserved = false,
                            Seats = 8,
                            ClientId = 2
                        },
                        new Tablee()
                        {
                            Busy = true,
                            Reserved = false,
                            Seats = 12,
                            ClientId = 2
                        },
                        new Tablee()
                        {
                            Busy = true,
                            Reserved = false,
                            Seats = 4,
                            ClientId = 2
                        },
                        new Tablee()
                        {
                            Busy = true,
                            Reserved = false,
                            Seats = 4,
                            ClientId = 2
                        },
                        new Tablee()
                        {
                            Busy = true,
                            Reserved = false,
                            Seats = 4,
                            ClientId = 3
                        },
                        new Tablee()
                        {
                            Busy = false,
                            Reserved = false,
                            Seats = 2,
                            ClientId = 3
                        },
                        new Tablee()
                        {
                            Busy = false,
                            Reserved = true,
                            Seats = 2,
                            ClientId = 3
                        },
                        new Tablee()
                        {
                            Busy = false,
                            Reserved = false,
                            Seats = 2,
                            ClientId = 3
                        },
                        new Tablee()
                        {
                            Busy = false,
                            Reserved = true,
                            Seats = 2,
                            ClientId = 3
                        },
                        new Tablee()
                        {
                            Busy = false,
                            Reserved = true,
                            Seats = 14,
                            ClientId = 3
                        },
                        new Tablee()
                        {
                            Busy = true,
                            Reserved = false,
                            Seats = 20,
                            ClientId = 3
                        },
                    }
                },


                new Restaurant()
                {
                    Name="Śniadaniownia",
                    Description="Delikatesy u Teresy",
                    HasDelivery=false,
                    ContactEmail="Tereska.Contact@wp.pl",
                    ContactNumber="211 507 009",
                    Address = new Address()
                    {
                        City = "Białystok",
                        Street = "Śniadaniowa 69",
                        PostalCode = "30-032"
                    },
                    Menu = new List<Dish>
                    {
                        new Dish()
                        {
                            Name = "Naleśniki z syropem",
                            Description = "Naleśnik",
                            Price = 22,
                            DishType = DishType.Cookies,
                        },
                        new Dish()
                        {
                            Name = "Gofry",
                            Description = "Gofry",
                            Price = 15,
                            DishType = DishType.Cookies,
                        },
                        new Dish()
                        {
                            Name = "Jajecznica",
                            Description = "Jajka",
                            Price = 19,
                            DishType = DishType.Egg,
                        },
                        new Dish()
                        {
                            Name = "Jajko sadzone",
                            Description = "Jajko",
                            Price = 19,
                            DishType = DishType.Egg,
                        },
                        new Dish()
                        {
                            Name = "Pasztet jajeczny",
                            Description = "Pasztet",
                            Price = 33,
                            DishType = DishType.Egg,
                        },
                        new Dish()
                        {
                            Name = "Placki czekoladowe",
                            Description = "Placki",
                            Price = 22,
                            DishType= DishType.Dessert,
                        },
                        new Dish()
                        {
                            Name = "Pieczona Owsianka",
                            Description = "Owsianka",
                            Price = 22,
                            DishType= DishType.Vegan,
                        },
                        new Dish()
                        {
                            Name = "Zawijasy z łososia",
                            Description = "Ryba",
                            Price = 35,
                            DishType= DishType.Fish,
                        },
                        new Dish()
                        {
                            Name = "Sałatka",
                            Description = "Sałatka",
                            Price = 21,
                            DishType= DishType.Vegan,
                        },
                        new Dish()
                        {
                            Name = "Spaghetti",
                            Description = "Makaron",
                            Price = 25,
                            DishType= DishType.Pasta,
                        },
                        new Dish()
                        {
                            Name = "Kozel",
                            Description = "Piwko",
                            Price = 9,
                            DishType= DishType.Alcohol,
                        },
                        new Dish()
                        {
                            Name = "Żóbrówka",
                            Description = "Wudeczka",
                            Price = 14,
                            DishType= DishType.Alcohol,
                        },
                        new Dish()
                        {
                            Name = "Herbata ziomowa",
                            Description = "Herbata",
                            Price = 9,
                            DishType= DishType.Drinks,
                        },
                        new Dish()
                        {
                            Name = "Herbata na zimno",
                            Description = "Herbata",
                            Price = 13,
                            DishType= DishType.Drinks,
                        },
                        new Dish()
                        {
                            Name = "Kawa z anyżem",
                            Description = "Kawa",
                            Price = 13,
                            DishType= DishType.Drinks,
                        },
                        new Dish()
                        {
                            Name = "Kawa latte z syropem ",
                            Description = "Kawa",
                            Price = 14,
                            DishType= DishType.Drinks,
                        },
                        new Dish()
                        {
                            Name = "Kawa espresso",
                            Description = "Kawa",
                            Price = 9,
                            DishType= DishType.Drinks,
                        },
                        new Dish()
                        {
                            Name = "Deser lodwy",
                            Description = "Lody",
                            Price = 15,
                            DishType= DishType.Dessert,
                        },
                        new Dish()
                        {
                            Name = "Ręcznie wyciskany sok",
                            Description = "Napój",
                            Price = 17,
                            DishType= DishType.Drinks,
                        },
                    },
                    Workers = new List<Worker>
                    {
                        new Worker()
                        {
                            Email = "ronaldinioGauczo@wp.pl",
                            FirstName = "Ronalidnio",
                            LastName = "Gauczo",
                            DateOfBirth = DateTime.Today,
                            RoleId = 1,
                            Rating = 5,
                            PasswordHash = "123"
                        },
                         new Worker()
                        {
                            Email = "karimMbappe@wp.pl",
                            FirstName = "Karim",
                            LastName = "Mbappe",
                            DateOfBirth = DateTime.Today,
                            RoleId = 2,
                            Rating = 5,
                            PasswordHash = "123"
                        },
                        new Worker()
                        {
                            Email = "kylianBenzema@wp.pl",
                            FirstName = "Kylian",
                            LastName = "Benzema",
                            DateOfBirth = DateTime.Today,
                            RoleId = 3,
                            Rating = 5,
                            PasswordHash = "123"
                        },
                        new Worker()
                        {
                            Email = "mariuszT@wp.pl",
                            FirstName = "Mariusz",
                            LastName = "Tarantula",
                            DateOfBirth = DateTime.Today,
                            RoleId = 2,
                            Rating = 5,
                            PasswordHash = "123"
                        },
                        new Worker()
                        {
                            Email = "marlenkaOrzel@wp.pl",
                            FirstName = "Marlenka",
                            LastName = "Orzel",
                            DateOfBirth = DateTime.Today,
                            RoleId = 3,
                            Rating = 5,
                            PasswordHash = "123"
                        }
                    },
                    Clients = new List<Client>()
                    {
                        new Client()
                        {
                            Email = "maciekLuj@wp.pl",
                            FirstName = "Maciej",
                            LastName = "Luj",
                            DateOfBirth = DateTime.Today,
                            PasswordHash = "123"
                        },
                        new Client()
                        {
                            Email = "jacekPasut@wp.pl",
                            FirstName = "Jacek",
                            LastName = "Pasut",
                            DateOfBirth = DateTime.Today,
                            PasswordHash = "123"
                        },
                        new Client()
                        {
                            Email = "JanŁborodo@wp.pl",
                            FirstName = "Jan",
                            LastName = "Łborodo",
                            DateOfBirth = DateTime.Today,
                            PasswordHash = "123"
                        },
                    },
                    Tables = new List<Tablee>()
                    {
                        new Tablee()
                        {
                            Busy = true,
                            Reserved = false,
                            Seats = 8,
                            ClientId = 2
                        },
                        new Tablee()
                        {
                            Busy = true,
                            Reserved = false,
                            Seats = 8,
                            ClientId = 2
                        },
                        new Tablee()
                        {
                            Busy = true,
                            Reserved = false,
                            Seats = 12,
                            ClientId = 2
                        },
                        new Tablee()
                        {
                            Busy = true,
                            Reserved = false,
                            Seats = 4,
                            ClientId = 2
                        },
                        new Tablee()
                        {
                            Busy = true,
                            Reserved = false,
                            Seats = 4,
                            ClientId = 2
                        },
                        new Tablee()
                        {
                            Busy = true,
                            Reserved = false,
                            Seats = 4,
                            ClientId = 3
                        },
                        new Tablee()
                        {
                            Busy = false,
                            Reserved = false,
                            Seats = 2,
                            ClientId = 3
                        },
                        new Tablee()
                        {
                            Busy = false,
                            Reserved = true,
                            Seats = 2,
                            ClientId = 3
                        },
                        new Tablee()
                        {
                            Busy = false,
                            Reserved = false,
                            Seats = 2,
                            ClientId = 3
                        },
                        new Tablee()
                        {
                            Busy = false,
                            Reserved = true,
                            Seats = 2,
                            ClientId = 3
                        },
                        new Tablee()
                        {
                            Busy = false,
                            Reserved = true,
                            Seats = 14,
                            ClientId = 3
                        },
                        new Tablee()
                        {
                            Busy = true,
                            Reserved = false,
                            Seats = 20,
                            ClientId = 3
                        },
                        new Tablee()
                        {
                            Busy = false,
                            Reserved = true,
                            Seats = 4,
                            ClientId = 3
                        },
                        new Tablee()
                        {
                            Busy = true,
                            Reserved = false,
                            Seats = 4,
                            ClientId = 3
                        }
                    }
                },


                new Restaurant()
                {
                    Name="Pizza prosto z pieca",
                    Description="Pizza włoska",
                    HasDelivery=false,
                    ContactEmail="PizzaP.Contact@wp.pl",
                    ContactNumber="697 880 540",
                    Address = new Address()
                    {
                        City = "Białystok",
                        Street = "Szkolna 17",
                        PostalCode = "30-032"
                    },
                    Menu = new List<Dish>
                    {
                        new Dish()
                        {
                            Name = "Crema Di Pomodoro",
                            Description = "Pizza pomidorowa",
                            Price = 16,
                            DishType = DishType.Pizza,
                        },
                        new Dish()
                        {
                            Name = "Facaccia",
                            Description = "Pizza vegańska",
                            Price = 16,
                            DishType = DishType.Vegan,
                        },
                        new Dish()
                        {
                            Name = "Pancetta",
                            Description = "Pizza",
                            Price = 35,
                            DishType = DishType.Pizza,
                        },
                        new Dish()
                        {
                            Name = "Capricciosa",
                            Description = "Pizza z pieczarkami",
                            Price = 39,
                            DishType = DishType.Pizza,
                        },
                        new Dish()
                        {
                            Name = "Diavola",
                            Description = "Pizza ostra",
                            Price = 35,
                            DishType = DishType.Pizza,
                        },
                        new Dish()
                        {
                            Name = "Salmone pizza",
                            Description = "Pizza z łososiem",
                            Price = 42,
                            DishType= DishType.Pizza,
                        },
                        new Dish()
                        {
                            Name = "Pizza cheetos",
                            Description = "Pizza z chipsami",
                            Price = 22,
                            DishType= DishType.Pizza,
                        },
                        new Dish()
                        {
                            Name = "Zawijasy z łososia",
                            Description = "Ryba",
                            Price = 35,
                            DishType= DishType.Fish,
                        },
                        new Dish()
                        {
                            Name = "San Daniele",
                            Description = "Pizza",
                            Price = 38,
                            DishType= DishType.Pizza,
                        },
                        new Dish()
                        {
                            Name = "Pizza mięsna",
                            Description = "Pizza 4 mięsa",
                            Price = 45,
                            DishType= DishType.Pizza,
                        },
                        new Dish()
                        {
                            Name = "Serowa",
                            Description = "Pizza 4 sery",
                            Price = 38,
                            DishType= DishType.Pizza,
                        },
                        new Dish()
                        {
                            Name = "Żóbrówka",
                            Description = "Wudeczka",
                            Price = 14,
                            DishType= DishType.Alcohol,
                        },
                        new Dish()
                        {
                            Name = "Kozel jasny",
                            Description = "Piwo",
                            Price = 9,
                            DishType= DishType.Alcohol,
                        },
                        new Dish()
                        {
                            Name = "Kozel ciemny",
                            Description = "Piwo",
                            Price = 11,
                            DishType= DishType.Alcohol,
                        },
                        new Dish()
                        {
                            Name = "Książęce lager",
                            Description = "Piwo",
                            Price = 13,
                            DishType= DishType.Alcohol,
                        },
                        new Dish()
                        {
                            Name = "Coca Cola",
                            Description = "Napój",
                            Price = 9,
                            DishType= DishType.Drinks,
                        },
                        new Dish()
                        {
                            Name = "Fuztea",
                            Description = "Napój",
                            Price = 8,
                            DishType= DishType.Drinks,
                        },
                        new Dish()
                        {
                            Name = "Dzbanek Lemoniady",
                            Description = "Napój",
                            Price = 18,
                            DishType= DishType.Drinks,
                        },
                    },
                    Workers = new List<Worker>
                    {
                        new Worker()
                        {
                            Email = "patListonosz@wp.pl",
                            FirstName = "Pat",
                            LastName = "Listonosz",
                            DateOfBirth = DateTime.Today,
                            RoleId = 1,
                            Rating = 5,
                            PasswordHash = "123"
                        },
                         new Worker()
                        {
                            Email = "kubusMaruda@wp.pl",
                            FirstName = "Jakub",
                            LastName = "Maruda",
                            DateOfBirth = DateTime.Today,
                            RoleId = 2,
                            Rating = 5,
                            PasswordHash = "123"
                        },
                        new Worker()
                        {
                            Email = "maniekRadek@wp.pl",
                            FirstName = "Maniek",
                            LastName = "Radek",
                            DateOfBirth = DateTime.Today,
                            RoleId = 3,
                            Rating = 5,
                            PasswordHash = "123"
                        },
                        new Worker()
                        {
                            Email = "royLewandowski@wp.pl",
                            FirstName = "Roy",
                            LastName = "Lewandowski",
                            DateOfBirth = DateTime.Today,
                            RoleId = 2,
                            Rating = 5,
                            PasswordHash = "123"
                        },
                        new Worker()
                        {
                            Email = "wiktoriaSpocona@wp.pl",
                            FirstName = "Wiktoria",
                            LastName = "Spocona",
                            DateOfBirth = DateTime.Today,
                            RoleId = 3,
                            Rating = 5,
                            PasswordHash = "123"
                        }
                    },
                    Clients = new List<Client>()
                    {
                        new Client()
                        {
                            Email = "barbaraHuda@wp.pl",
                            FirstName = "Barbara",
                            LastName = "Huda",
                            DateOfBirth = DateTime.Today,
                            PasswordHash = "123"
                        },
                        new Client()
                        {
                            Email = "maxMad@wp.pl",
                            FirstName = "Mad",
                            LastName = "Max",
                            DateOfBirth = DateTime.Today,
                            PasswordHash = "123"
                        },
                        new Client()
                        {
                            Email = "krzysztofSuchodolski@wp.pl",
                            FirstName = "Krzysztof",
                            LastName = "Suchodolski",
                            DateOfBirth = DateTime.Today,
                            PasswordHash = "123"
                        },
                    },
                    Tables = new List<Tablee>()
                    {
                        new Tablee()
                        {
                            Busy = true,
                            Reserved = false,
                            Seats = 8,
                            ClientId = 2
                        },
                        new Tablee()
                        {
                            Busy = true,
                            Reserved = false,
                            Seats = 8,
                            ClientId = 2
                        },
                        new Tablee()
                        {
                            Busy = true,
                            Reserved = false,
                            Seats = 12,
                            ClientId = 2
                        },
                        new Tablee()
                        {
                            Busy = true,
                            Reserved = false,
                            Seats = 4,
                            ClientId = 2
                        },
                        new Tablee()
                        {
                            Busy = true,
                            Reserved = false,
                            Seats = 4,
                            ClientId = 2
                        },
                        new Tablee()
                        {
                            Busy = true,
                            Reserved = false,
                            Seats = 4,
                            ClientId = 3
                        },
                        new Tablee()
                        {
                            Busy = false,
                            Reserved = false,
                            Seats = 2,
                            ClientId = 3
                        },
                        new Tablee()
                        {
                            Busy = false,
                            Reserved = true,
                            Seats = 2,
                            ClientId = 3
                        },
                        new Tablee()
                        {
                            Busy = false,
                            Reserved = false,
                            Seats = 2,
                            ClientId = 3
                        },
                        new Tablee()
                        {
                            Busy = false,
                            Reserved = true,
                            Seats = 2,
                            ClientId = 3
                        },
                        new Tablee()
                        {
                            Busy = false,
                            Reserved = true,
                            Seats = 14,
                            ClientId = 3
                        },
                        new Tablee()
                        {
                            Busy = true,
                            Reserved = false,
                            Seats = 20,
                            ClientId = 3
                        },
                        new Tablee()
                        {
                            Busy = false,
                            Reserved = true,
                            Seats = 4,
                            ClientId = 3
                        },
                        new Tablee()
                        {
                            Busy = true,
                            Reserved = false,
                            Seats = 4,
                            ClientId = 3
                        },
                        new Tablee()
                        {
                            Busy = false,
                            Reserved = true,
                            Seats = 2,
                            ClientId = 3
                        },
                        new Tablee()
                        {
                            Busy = true,
                            Reserved = false,
                            Seats = 2,
                            ClientId = 3
                        }
                    }
                }
            };
        }
    }
}
