using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Dish : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public Category Category { get; set; }
    }
}