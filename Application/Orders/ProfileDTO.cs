using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Orders
{
    public class ProfileDTO<TKey>
    {
        public TKey Id { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public bool IsAdmin { get; set; }
    }
}