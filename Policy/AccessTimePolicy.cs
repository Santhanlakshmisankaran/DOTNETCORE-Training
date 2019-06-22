using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trainingmiddleware.Policy
{
    public class AccessTimeRequirment : IAuthorizationRequirement
    {
        public int MinTime { get; set; }

        public AccessTimeRequirment(int minTime)
        {
            MinTime = minTime;

        }
    }
    public class AccessTimeHandler : AuthorizationHandler<AccessTimeRequirment>   
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AccessTimeRequirment requirement)
        {
            var ctime = context.User.FindFirst("creationTime");// context - token payload not DB context
            if(ctime == null)
            {
                return Task.CompletedTask;
            }
            var time = Math.Abs(TimeSpan.FromTicks(Convert.ToInt64(ctime.Value)).Subtract(TimeSpan.FromTicks(DateTime.Now.Ticks)).Minutes);

            if (time > requirement.MinTime)
                context.Succeed(requirement);
            else
                context.Fail();

            return Task.CompletedTask;
        }
    }
}
