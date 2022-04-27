using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Persistence
{
    // need a refactor !!!
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (!context.Dishes.Any() && !context.Categories.Any())
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
                await context.Dishes.AddRangeAsync(dishes);
                await context.Portions.AddRangeAsync(portions);
                await context.SaveChangesAsync();
            }
        }
    }
}
