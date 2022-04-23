using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class BaseEntity<TKey>   
    {
        public TKey Id { get; set; }
    }
}