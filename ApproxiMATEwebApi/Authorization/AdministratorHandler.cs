using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi.Authorization
{
    public class AdministratorHandler : AuthorizationHandler<AdministratorRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdministratorRequirement requirement)
        {
            //throw new NotImplementedException();
            if(!context.User.HasClaim(c=> c.Type == Constants.ApplicationUserAccountTypeClaim 
                                          && c.Issuer == Constants.HostedWebApiAddress))
            {
                return Task.CompletedTask;
            }
            var accountType = context.User.FindFirst(c => c.Type == Constants.ApplicationUserAccountTypeClaim
                                                          && c.Issuer == Constants.HostedWebApiAddress).Value;
            if (accountType.Equals("Administrator"))
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
