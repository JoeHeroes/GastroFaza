using GastroFaza.Models;
using GastroFaza.Models.Enum;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

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
               
                if (!dbContext.Dishs.Any())
                {
                    var dishes = GetDishes();
                    dbContext.Dishs.AddRange(dishes);
                    dbContext.SaveChanges();
                }

                if (!dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurant();
                    dbContext.Restaurants.AddRange(restaurants);
                    dbContext.SaveChanges();
                }

                if (!dbContext.Tables.Any())
                {
                    var tables = GetTables();
                    dbContext.Tables.AddRange(tables);
                    dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<DiningTable> GetTables()
        {
            return new List<DiningTable> ()
            {
                new DiningTable()
                {
                    Busy= true,
                    Seats =6,
                },
                new DiningTable()
                {
                    Busy= true,
                    Seats =2,
                },
                new DiningTable()
                {
                    Busy= true,
                    Seats =3,
                },
                new DiningTable()
                {
                    Busy= true,
                    Seats =3,
                },
            };
        }


        private IEnumerable<Dish> GetDishes()
        {
            return new List<Dish>()
            {
                new Dish()
                        {
                            Name = "Margarita",
                            Description = "Margarita",
                            Price = 15.5,
                            DishType = DishType.Pizza,
                            ProfileImg = "Pizza/Margharita.png"
                        },
                        new Dish()
                        {
                            Name = "Pasta with Tomato",
                            Description = "Tomato",
                            Price = 24.5,
                            DishType = DishType.Pasta,
                            ProfileImg = "Makarony/PastaWithTomatoe.png"
                        },
                        new Dish()
                        {
                            Name = "Pasta with Tomato",
                            Description = "Tomato",
                            Price = 24.5,
                            DishType = DishType.Pasta,
                            ProfileImg = "Makarony/PastaWithTomatoe.png"
                        },
                        new Dish()
                        {
                            Name = "ChesseBurger",
                            Description = "Burger",
                            Price = 29,
                            DishType = DishType.Burger,
                            ProfileImg = "Burgery/CheeseBurger.png"
                        },
                        new Dish()
                        {
                            Name = "Burger Kimchi",
                            Description = "Burger",
                            Price = 36,
                            DishType = DishType.Burger,
                            ProfileImg = "Burgery/BurgerKimchi.png"
                        },
                        new Dish()
                        {
                            Name = "Cezar",
                            Description = "Sałatka",
                            Price = 33,
                            DishType= DishType.Salad,
                            ProfileImg = "Salatki/Cezar.png"
                        },
                        new Dish()
                        {
                            Name = "Leczo",
                            Description = "Makaron",
                            Price = 34,
                            DishType= DishType.Pasta,
                            ProfileImg = "Makarony/Leczo.png"
                        },
                        new Dish()
                        {
                            Name = "Tom Yum",
                            Description = "Zupa",
                            Price = 29,
                            DishType= DishType.Soup,
                            ProfileImg = "Zupy/TomYum.png"
                        },
                        new Dish()
                        {
                            Name = "Pad-Thai z krewetkami",
                            Description = "Makaron",
                            Price = 55,
                            DishType= DishType.Pasta,
                            ProfileImg = "Makarony/PadThai.png"
                        },
                        new Dish()
                        {
                            Name = "Ramen",
                            Description = "Makaron",
                            Price = 37,
                            DishType= DishType.Pasta,
                            ProfileImg = "Makarony/Ramen.png"
                        },
                        new Dish()
                        {
                            Name = "Lava Cake",
                            Description = "Deser",
                            Price = 22,
                            DishType= DishType.Dessert,
                            ProfileImg = "Desery/LavaCake.png"
                        },
                        new Dish()
                        {
                            Name = "Old Fashioned",
                            Description = "Drink",
                            Price = 26,
                            DishType= DishType.Alcohol,
                            ProfileImg = "Alkohole/OldFashioned.png"
                        },
                        new Dish()
                        {
                            Name = "Aperol Spritz",
                            Description = "Drink",
                            Price = 27,
                            DishType= DishType.Alcohol,
                            ProfileImg = "Alkohole/AperolSpritz.png"
                        },
                        new Dish()
                        {
                            Name = "Cuba Libre",
                            Description = "Drink",
                            Price = 23,
                            DishType= DishType.Alcohol,
                            ProfileImg = "Alkohole/CubaLibre.png"
                        },
                        new Dish()
                        {
                            Name = "Negroni",
                            Description = "Drink",
                            Price = 27,
                            DishType= DishType.Alcohol,
                            ProfileImg = "Alkohole/Negroni.png"
                        },
                        new Dish()
                        {
                            Name = "Coca Cola",
                            Description = "Napój",
                            Price = 10,
                            DishType= DishType.Drinks,
                            ProfileImg = "Napoje/CocaCola.png"
                        },
                        new Dish()
                        {
                            Name = "Fanta",
                            Description = "Napój",
                            Price = 10,
                            DishType= DishType.Drinks,
                            ProfileImg = "Napoje/Fanta.png"
                        },
                        new Dish()
                        {
                            Name = "Cappy",
                            Description = "Napój",
                            Price = 8,
                            DishType= DishType.Drinks,
                            ProfileImg = "Napoje/Cappy.png"
                        },
                        new Dish()
                        {
                            Name = "Carpaccio z polędwicy wołowej",
                            Description = "Carpaccio",
                            Price = 42,
                            DishType = DishType.Pasta,
                            ProfileImg = "Makarony/Carpaccio.png"
                        },
                        new Dish()
                        {
                            Name = "Tatar",
                            Description = "Tatar",
                            Price = 52,
                            DishType = DishType.Meat,
                            ProfileImg = "Mieso/Tatar.png"
                        },
                        new Dish()
                        {
                            Name = "Rosół",
                            Description = "Rosół",
                            Price = 19,
                            DishType = DishType.Soup,
                            ProfileImg = "Zupy/Rosol.png"
                        },
                        new Dish()
                        {
                            Name = "Sałatka cezar z łososiem",
                            Description = "sałatka",
                            Price = 46,
                            DishType = DishType.Salad,
                            ProfileImg = "Salatki/CezarZLososiem.png"
                        },
                        new Dish()
                        {
                            Name = "Blackened Chicken",
                            Description = "Kurczak",
                            Price = 42,
                            DishType = DishType.Meat,
                            ProfileImg = "Mieso/BlackenedChicken.png"
                        },
                        new Dish()
                        {
                            Name = "Żeberka BBQ",
                            Description = "Żeberka",
                            Price = 47,
                            DishType= DishType.Meat,
                            ProfileImg = "Mieso/ZeberkaBBQ.png"
                        },
                        new Dish()
                        {
                            Name = "Polędwiczka wieprzowa",
                            Description = "Polędwiczka",
                            Price = 47,
                            DishType= DishType.Meat,
                            ProfileImg = "Mieso/PoledwiczkaWieprzowa.png"
                        },
                        new Dish()
                        {
                            Name = "Tarta z jabłkami",
                            Description = "Tarta",
                            Price = 30,
                            DishType= DishType.Dessert,
                            ProfileImg = "Desery/TartaZJabłkami.png"
                        },
                        new Dish()
                        {
                            Name = "Malinowa chmurka",
                            Description = "Ciasto z malinami",
                            Price = 21,
                            DishType= DishType.Dessert,
                            ProfileImg = "Ciasta/MalinowaChmurka.png"
                        },
                        new Dish()
                        {
                            Name = "Spaghetti",
                            Description = "Makaron",
                            Price = 25,
                            DishType= DishType.Pasta,
                            ProfileImg = "Makarony/SpaghettiBolognese.png"
                        },
                        new Dish()
                        {
                            Name = "Sernik",
                            Description = "Sernik",
                            Price = 19,
                            DishType= DishType.Dessert,
                            ProfileImg = "Ciasta/Sernik.png"
                        },
                        new Dish()
                        {
                            Name = "Crema Di Pomodoro",
                            Description = "Pizza pomidorowa",
                            Price = 16,
                            DishType = DishType.Pizza,
                            ProfileImg = "Zupy/CremaDiPomodoro.png"
                        },
                        new Dish()
                        {
                            Name = "Facaccia",
                            Description = "Pizza vegańska",
                            Price = 16,
                            DishType = DishType.Vegan,
                            ProfileImg = "Vegan/Focaccia.png"
                        },
                        new Dish()
                        {
                            Name = "Pancetta",
                            Description = "Pizza",
                            Price = 35,
                            DishType = DishType.Pizza,
                            ProfileImg = "Pizza/Pancetta.png"
                        },
                        new Dish()
                        {
                            Name = "Capricciosa",
                            Description = "Pizza z pieczarkami",
                            Price = 39,
                            DishType = DishType.Pizza,
                            ProfileImg = "Pizza/Capricciosa.png"
                        },
                        new Dish()
                        {
                            Name = "Diavola",
                            Description = "Pizza ostra",
                            Price = 35,
                            DishType = DishType.Pizza,
                            ProfileImg = "Pizza/Diavolo.png"
                        },
                        new Dish()
                        {
                            Name = "Salmone pizza",
                            Description = "Pizza z łososiem",
                            Price = 42,
                            DishType= DishType.Pizza,
                            ProfileImg = "Pizza/Salmone.png"
                        },
                        new Dish()
                        {
                            Name = "Pizza cheetos",
                            Description = "Pizza z chipsami",
                            Price = 22,
                            DishType= DishType.Pizza,
                            ProfileImg = "Pizza/Cheetos.png"
                        },
                        new Dish()
                        {
                            Name = "Zawijasy z łososia",
                            Description = "Ryba",
                            Price = 35,
                            DishType= DishType.Fish,
                            ProfileImg = "Ryby/SalmonRool.png"
                        },
                        new Dish()
                        {
                            Name = "San Daniele",
                            Description = "Pizza",
                            Price = 38,
                            DishType= DishType.Pizza,
                            ProfileImg = "Pizza/SanDaniele.png"
                        },
                        new Dish()
                        {
                            Name = "Pizza mięsna",
                            Description = "Pizza 4 mięsa",
                            Price = 45,
                            DishType= DishType.Pizza,
                            ProfileImg = "Pizza/Miesna.png"
                        },
                        new Dish()
                        {
                            Name = "Serowa",
                            Description = "Pizza 4 sery",
                            Price = 38,
                            DishType= DishType.Pizza,
                            ProfileImg = "Pizza/Serowa.png"
                        },
                        new Dish()
                        {
                            Name = "Żóbrówka",
                            Description = "Wudeczka",
                            Price = 14,
                            DishType= DishType.Alcohol,
                            ProfileImg = "Alkohole/Zobrowka.png"
                        },
                        new Dish()
                        {
                            Name = "Kozel jasny",
                            Description = "Piwo",
                            Price = 9,
                            DishType= DishType.Alcohol,
                            ProfileImg = "Alkohole/KozelJasny.png"
                        },
                        new Dish()
                        {
                            Name = "Kozel ciemny",
                            Description = "Piwo",
                            Price = 11,
                            DishType= DishType.Alcohol,
                            ProfileImg = "Alkohole/KozelCiemny.png"
                        },
                        new Dish()
                        {
                            Name = "Książęce lager",
                            Description = "Piwo",
                            Price = 13,
                            DishType= DishType.Alcohol,
                            ProfileImg = "Alkohole/Ksiazece.png"
                        },
                        new Dish()
                        {
                            Name = "Coca Cola",
                            Description = "Napój",
                            Price = 9,
                            DishType= DishType.Drinks,
                            ProfileImg = "Napoje/CocaCola.png"
                        },
                        new Dish()
                        {
                            Name = "Fuztea",
                            Description = "Napój",
                            Price = 8,
                            DishType= DishType.Drinks,
                            ProfileImg = "Napoje/Fuztea.png"
                        },
                        new Dish()
                        {
                            Name = "Dzbanek Lemoniady",
                            Description = "Napój",
                            Price = 18,
                            DishType= DishType.Drinks,
                            ProfileImg = "Napoje/DzbanekLemoniady.png"
                        },
                        new Dish()
                        {
                            Name = "Kozel",
                            Description = "Drink",
                            Price = 10,
                            DishType= DishType.Alcohol,
                            ProfileImg = "Alkohole/KozelJasny.png"
                        },
                        new Dish()
                        {
                            Name = "Książęce",
                            Description = "Drink",
                            Price = 9,
                            DishType= DishType.Alcohol,
                            ProfileImg = "Alkohole/Ksiazece.png"
                        },
                        new Dish()
                        {
                            Name = "Jagermeister",
                            Description = "Drink",
                            Price = 13,
                            DishType= DishType.Alcohol,
                            ProfileImg = "Alkohole/Jagermaister.png"
                        },
                        new Dish()
                        {
                            Name = "Jack Daniels (burbon)",
                            Description = "Drink",
                            Price = 16,
                            DishType= DishType.Alcohol,
                            ProfileImg = "Alkohole/JackDaniels.png"
                        },
                        new Dish()
                        {
                            Name = "Coca Cola",
                            Description = "Napój",
                            Price = 10,
                            DishType= DishType.Drinks,
                            ProfileImg = "Napoje/CocaCola.png"
                        },
                        new Dish()
                        {
                            Name = "Fanta",
                            Description = "Napój",
                            Price = 10,
                            DishType= DishType.Drinks,
                            ProfileImg = "Napoje/Fanta.png"
                        },
                        new Dish()
                        {
                            Name = "Cappy",
                            Description = "Napój",
                            Price = 8,
                            DishType= DishType.Drinks,
                            ProfileImg = "Napoje/Cappy.png"
                        },

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



        private IEnumerable<Restaurant> GetRestaurant()
        {
            return new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "ChujaCzita",
                    Description="Niebo w gebie",
                    HasDelivery= true,
                    ContactEmail = "GastroFaza@wp.pl",
                    ContactNumber= "667 676 776",
                    Address= new Address()
                    {
                        City = "Gastro",
                        PostalCode="11-100",
                        Street ="Faza"
                    }
                },
                
            };
        }
       


        private IEnumerable<Role> GetRoles()
        {
            return new List<Role>()
            {
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
                 },
            };
        }
    }
}
