using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<User> userManager)
        {
            if (!userManager.Users.Any()) {
                var users = new List<User> {
                    new User {DisplayName = "Tom", UserName = "tom", Email = "tom@gmail.com"},
                    new User {DisplayName = "Dmytro", UserName = "dmytro", Email = "dmytro@gmail.com"},
                    new User {DisplayName = "Admin", UserName = "admin", Email = "admin@gmail.com", IsAdmin = true},

                };
                await userManager.CreateAsync(users[0], "Tom1234$");
                await userManager.CreateAsync(users[1], "Dmytro1234$");
                await userManager.CreateAsync(users[2], "Admin1234$");
            }

            if (!context.Categories.Any() && !context.Ingredients.Any())
            {

                var categories = new List<Category>
                {
                    new Category { Name = "Salads" },
                    new Category { Name = "Soups" },
                    new Category { Name = "Bruschettas" },
                    new Category { Name = "Meat" },
                    new Category { Name = "Pizza" }
                };

                var ingredients = new List<Ingredient> {
                    new Ingredient { Name = "Cherry tomatoes"},
                    new Ingredient { Name = "Spinach"},
                    new Ingredient { Name = "Olives"},
                    new Ingredient { Name = "Chicken"},
                    new Ingredient { Name = "Salmon"},
                    new Ingredient { Name = "Broccoli"},
                    new Ingredient { Name = "Champignons"},
                    new Ingredient { Name = "Onion"},
                    new Ingredient { Name = "Carrot"},
                    new Ingredient { Name = "Squid"},
                    new Ingredient { Name = "Bread"},
                    new Ingredient { Name = "Salami Milano"},
                    new Ingredient { Name = "Duck breasts"},
                    new Ingredient { Name = "Kosher salt"},
                    new Ingredient { Name = "White wine"},
                    new Ingredient { Name = "Peper sauce"},
                    new Ingredient { Name = "Oil"},
                    new Ingredient { Name = "Mozzarella"},
                    new Ingredient { Name = "Salami pepperoni"}
                };

                await context.Categories.AddRangeAsync(categories);
                await context.Ingredients.AddRangeAsync(ingredients);
                await context.SaveChangesAsync();
            }
        }
    }
}
