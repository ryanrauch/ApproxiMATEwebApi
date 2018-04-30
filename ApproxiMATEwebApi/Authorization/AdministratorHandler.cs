using ApproxiMATEwebApi.Helpers;
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
            if(!context.User.HasClaim(c=> c.Type == Constants.ApplicationUserAccountTypeClaim))
            {
                return Task.CompletedTask;
            }
            var accountType = context.User.FindFirst(c => c.Type == Constants.ApplicationUserAccountTypeClaim).Value;
            if (accountType.Equals(AccountType.Administrative.ToString()))
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
