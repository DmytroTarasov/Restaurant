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
        public ProfileDTO<string> User { get; set; }
        public ICollection<PortionDTO<Guid>> Portions { get; set; } = new List<PortionDTO<Guid>>();
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}