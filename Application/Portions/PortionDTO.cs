using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Portions
{
    public class PortionDTO<TKey>
    {
        public TKey Id { get; set; }
        public string Size { get; set; }
        public int Price { get; set; }
    }
}