using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Portion : BaseEntity<Guid>
    {
        public int Size { get; set; }
        public int Price { get; set; }
        public Dish Dish { get; set; }
    }
}