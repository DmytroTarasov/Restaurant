using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class User : IdentityUser
    {
        public string DisplayName { get; set; }

        public override string ToString()
        {
            return "Email: " + Email + ", Id: " + Id + ", UserName: " + UserName;
        }
    }
}