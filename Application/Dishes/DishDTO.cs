using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Categories;

namespace Application.Dishes
{
    public class DishDTO<TKey>
    {
        public TKey Id { get; set; }
        public string Name { get; set; }
        public CategoryDTO<Guid> Category { get; set; }
    }
}