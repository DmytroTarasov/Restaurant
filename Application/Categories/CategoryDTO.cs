using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Categories
{
    public class CategoryDTO<TKey>
    {
        public TKey Id { get; set; }
        public string Name { get; set; }
    }
}