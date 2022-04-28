using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Ingredients
{
    public class IngredientDTO<TKey>
    {
        public TKey Id { get; set; }
        public string Name { get; set; }
    }
}