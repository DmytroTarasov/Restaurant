using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Portions;
using Domain;

namespace Application.Orders
{
    public class OrderDTO<TKey>
    {
        public TKey Id { get; set; }
        // public string Username { get; set; }
        // public string DisplayName { get; set; }
        public User User { get; set; } // not good
        public ICollection<PortionDTO<Guid>> Portions { get; set; } = new List<PortionDTO<Guid>>();
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}