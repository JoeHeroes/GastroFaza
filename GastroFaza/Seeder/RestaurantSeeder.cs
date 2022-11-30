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
               
                if (!dbContext.Dishs.Any())
                {
                    var dishes = GetDishes();
                    dbContext.Dishs.AddRange(dishes);
                    dbContext.SaveChanges();
                }
            }
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
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Pasta with Tomato",
                            Description = "Tomato",
                            Price = 24.5,
                            DishType = DishType.Pasta,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Pasta with Tomato",
                            Description = "Tomato",
                            Price = 24.5,
                            DishType = DishType.Pasta,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "ChesseBurger",
                            Description = "Burger",
                            Price = 29,
                            DishType = DishType.Burger,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Burger Kimchi",
                            Description = "Burger",
                            Price = 36,
                            DishType = DishType.Burger,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Cezar",
                            Description = "Sałatka",
                            Price = 33,
                            DishType= DishType.Salad,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Leczo",
                            Description = "Makaron",
                            Price = 34,
                            DishType= DishType.Pasta,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Tom Yum",
                            Description = "Zupa",
                            Price = 29,
                            DishType= DishType.Soup,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Pad-Thai z krewetkami",
                            Description = "Makaron",
                            Price = 55,
                            DishType= DishType.Pasta,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Ramen",
                            Description = "Makaron",
                            Price = 37,
                            DishType= DishType.Pasta,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Lava Cake",
                            Description = "Deser",
                            Price = 22,
                            DishType= DishType.Dessert,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Old Fashioned",
                            Description = "Drink",
                            Price = 26,
                            DishType= DishType.Alcohol,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Aperol Spritz",
                            Description = "Drink",
                            Price = 27,
                            DishType= DishType.Alcohol,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Cuba Libre",
                            Description = "Drink",
                            Price = 23,
                            DishType= DishType.Alcohol,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Negroni",
                            Description = "Drink",
                            Price = 27,
                            DishType= DishType.Alcohol,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Coca Cola",
                            Description = "Napój",
                            Price = 10,
                            DishType= DishType.Drinks,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Fanta",
                            Description = "Napój",
                            Price = 10,
                            DishType= DishType.Drinks,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Cappy",
                            Description = "Napój",
                            Price = 8,
                            DishType= DishType.Drinks,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Carpaccio z polędwicy wołowej",
                            Description = "Carpaccio",
                            Price = 42,
                            DishType = DishType.Pasta,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Tatar",
                            Description = "Tatar",
                            Price = 52,
                            DishType = DishType.Meat,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Rosół",
                            Description = "Rosół",
                            Price = 19,
                            DishType = DishType.Soup,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Sałatka cezar z łososiem",
                            Description = "sałatka",
                            Price = 46,
                            DishType = DishType.Salad,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Blackened Chicken",
                            Description = "Kurczak",
                            Price = 42,
                            DishType = DishType.Meat,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Żeberka BBQ",
                            Description = "Żeberka",
                            Price = 47,
                            DishType= DishType.Meat,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Polędwiczka wieprzowa",
                            Description = "Polędwiczka",
                            Price = 47,
                            DishType= DishType.Meat,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Tarta z jabłkami",
                            Description = "Tarta",
                            Price = 30,
                            DishType= DishType.Dessert,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Malinowa chmurka",
                            Description = "Ciasto z malinami",
                            Price = 21,
                            DishType= DishType.Dessert,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Spaghetti",
                            Description = "Makaron",
                            Price = 25,
                            DishType= DishType.Pasta,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Sernik",
                            Description = "Sernik",
                            Price = 19,
                            DishType= DishType.Dessert,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Crema Di Pomodoro",
                            Description = "Pizza pomidorowa",
                            Price = 16,
                            DishType = DishType.Pizza,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Facaccia",
                            Description = "Pizza vegańska",
                            Price = 16,
                            DishType = DishType.Vegan,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Pancetta",
                            Description = "Pizza",
                            Price = 35,
                            DishType = DishType.Pizza,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Capricciosa",
                            Description = "Pizza z pieczarkami",
                            Price = 39,
                            DishType = DishType.Pizza,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Diavola",
                            Description = "Pizza ostra",
                            Price = 35,
                            DishType = DishType.Pizza,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Salmone pizza",
                            Description = "Pizza z łososiem",
                            Price = 42,
                            DishType= DishType.Pizza,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Pizza cheetos",
                            Description = "Pizza z chipsami",
                            Price = 22,
                            DishType= DishType.Pizza,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Zawijasy z łososia",
                            Description = "Ryba",
                            Price = 35,
                            DishType= DishType.Fish,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "San Daniele",
                            Description = "Pizza",
                            Price = 38,
                            DishType= DishType.Pizza,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Pizza mięsna",
                            Description = "Pizza 4 mięsa",
                            Price = 45,
                            DishType= DishType.Pizza,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Serowa",
                            Description = "Pizza 4 sery",
                            Price = 38,
                            DishType= DishType.Pizza,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Żóbrówka",
                            Description = "Wudeczka",
                            Price = 14,
                            DishType= DishType.Alcohol,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Kozel jasny",
                            Description = "Piwo",
                            Price = 9,
                            DishType= DishType.Alcohol,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Kozel ciemny",
                            Description = "Piwo",
                            Price = 11,
                            DishType= DishType.Alcohol,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Książęce lager",
                            Description = "Piwo",
                            Price = 13,
                            DishType= DishType.Alcohol,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Coca Cola",
                            Description = "Napój",
                            Price = 9,
                            DishType= DishType.Drinks,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Fuztea",
                            Description = "Napój",
                            Price = 8,
                            DishType= DishType.Drinks,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Dzbanek Lemoniady",
                            Description = "Napój",
                            Price = 18,
                            DishType= DishType.Drinks,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Kozel",
                            Description = "Drink",
                            Price = 10,
                            DishType= DishType.Alcohol,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Książęce",
                            Description = "Drink",
                            Price = 9,
                            DishType= DishType.Alcohol,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Jagermeister",
                            Description = "Drink",
                            Price = 13,
                            DishType= DishType.Alcohol,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Jack Daniels (burbon)",
                            Description = "Drink",
                            Price = 16,
                            DishType= DishType.Alcohol,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Coca Cola",
                            Description = "Napój",
                            Price = 10,
                            DishType= DishType.Drinks,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Fanta",
                            Description = "Napój",
                            Price = 10,
                            DishType= DishType.Drinks,
                            ProfileImg = ""
                        },
                        new Dish()
                        {
                            Name = "Cappy",
                            Description = "Napój",
                            Price = 8,
                            DishType= DishType.Drinks,
                            ProfileImg = ""
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
    }
}
