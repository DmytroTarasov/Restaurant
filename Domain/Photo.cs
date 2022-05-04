using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Photo : BaseEntity<string>
    {
        public string Url { get; set; }
    }
}