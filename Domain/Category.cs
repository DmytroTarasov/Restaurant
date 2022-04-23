using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Category : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public ICollection<Dish> Dishes { get; set; } = new List<Dish>();
    }
}