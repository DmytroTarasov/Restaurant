using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Persistence;

namespace Infrastructure.Policies
{
    public class IsAdminRequirement : IAuthorizationRequirement
    {
    }

    public class IsAdminRequirementHandler : AuthorizationHandler<IsAdminRequirement>
    {
        private readonly DataContext _dbContext;

        public IsAdminRequirementHandler(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsAdminRequirement requirement)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null) return Task.CompletedTask;

            var user = _dbContext.Users.FindAsync(userId).Result;

            if (user == null) return Task.CompletedTask;

            if (user.IsAdmin) context.Succeed(requirement); 

            return Task.CompletedTask;
        }
    }
}