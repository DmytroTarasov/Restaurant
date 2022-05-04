using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Order : BaseEntity<Guid>
    {
        public User User { get; set; }
        public ICollection<Portion> Portions { get; set; } = new List<Portion>();
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}