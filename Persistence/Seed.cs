using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (!context.Dishes.Any())
            {

                var categories = new List<Category>
                {
                    new Category { Name = "Pizza" },
                    new Category { Name = "Grill" },
                    new Category { Name = "Drinks" },
                    new Category { Name = "Soups" },
                    new Category { Name = "Salads" }
                };

                var dishes = new List<Dish>
                {
                    new Dish
                    {
                        Name="Dish 1",
                        Category = categories[0]
                    },
                    new Dish
                    {
                        Name="Dish 2",
                        Category = categories[1]
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

                await context.Categories.AddRangeAsync(categories);
                await context.Dishes.AddRangeAsync(dishes);
                await context.SaveChangesAsync();
            }
        }
    }
}
