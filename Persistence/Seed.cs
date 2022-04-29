using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    // need a refactor !!!
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<User> userManager)
        {
            if (!userManager.Users.Any()) {
                var users = new List<User> {
                    new User {DisplayName = "Bob", UserName = "bob", Email = "bob@gmail.com"},
                    new User {DisplayName = "Tom", UserName = "tom", Email = "tom@gmail.com"},
                    new User {DisplayName = "Dmytro", UserName = "dmytro", Email = "dmytro@gmail.com"},
                };
                // create users
                await userManager.CreateAsync(users[0], "Bob1234$");
                await userManager.CreateAsync(users[1], "Tom1234$");
                await userManager.CreateAsync(users[2], "Dmytro1234$");

            }

            if (!context.Dishes.Any() && !context.Categories.Any() && !context.Ingredients.Any())
            {

                var categories = new List<Category>
                {
                    new Category { Name = "Pizza" },
                    new Category { Name = "Grill" },
                    new Category { Name = "Drinks" },
                    new Category { Name = "Soups" },
                    new Category { Name = "Salads" }
                };

                var portions = new List<Portion> 
                {
                    new Portion { Size = "300g", Price = 15},
                    new Portion { Size = "500g", Price = 25},
                    new Portion { Size = "700g", Price = 35},
                    new Portion { Size = "300g", Price = 20},
                    new Portion { Size = "500g", Price = 35},
                    new Portion { Size = "700g", Price = 50}
                };

                var dishes = new List<Dish>
                {
                    new Dish
                    {
                        Name="Dish 1",
                        Category = categories[0],
                        Portions = new List<Portion> {
                            portions[0],
                            portions[1],
                            portions[2]
                        }
                    },
                    new Dish
                    {
                        Name="Dish 2",
                        Category = categories[1],
                        Portions = new List<Portion> {
                            portions[3],
                            portions[4],
                            portions[5]
                        }
                    },
                    new Dish
                    {
                        Name="Dish 3",
                        Category = categories[2]
                    },
                };

                var ingredients = new List<Ingredient> {
                    new Ingredient { Name = "ingredient1"},
                    new Ingredient { Name = "ingredient2"},
                    new Ingredient { Name = "ingredient3"},
                    new Ingredient { Name = "ingredient4"},
                    new Ingredient { Name = "ingredient5"},
                    new Ingredient { Name = "ingredient6"},
                };

                dishes[0].Ingredients.Add(ingredients[0]);
                dishes[0].Ingredients.Add(ingredients[1]);
                dishes[0].Ingredients.Add(ingredients[2]);

                dishes[1].Ingredients.Add(ingredients[2]);
                dishes[1].Ingredients.Add(ingredients[3]);
                dishes[1].Ingredients.Add(ingredients[4]);

                categories[0].Dishes.Add(dishes[0]);
                categories[1].Dishes.Add(dishes[1]);
                categories[2].Dishes.Add(dishes[2]);

                portions[0].Dish = dishes[0];
                portions[1].Dish = dishes[0];
                portions[2].Dish = dishes[0];

                portions[3].Dish = dishes[1];
                portions[4].Dish = dishes[1];
                portions[5].Dish = dishes[1];

                await context.Categories.AddRangeAsync(categories);
                await context.Ingredients.AddRangeAsync(ingredients);
                await context.Dishes.AddRangeAsync(dishes);
                await context.Portions.AddRangeAsync(portions);
                await context.SaveChangesAsync();
            }
        }
    }
}
