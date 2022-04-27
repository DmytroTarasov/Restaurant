using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Categories;
using Application.Photos;
using Application.Portions;

namespace Application.Dishes
{
    public class DishDTO<TKey>
    {
        public TKey Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CategoryDTO<Guid> Category { get; set; }
        public ICollection<PortionDTO<Guid>> Portions { get; set; }
        public PhotoDTO<string> Photo { get; set; }
    }
}