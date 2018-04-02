using ApproxiMATEwebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi.Authorization
{
    public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public ApplicationUserClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor) : base(userManager, roleManager, optionsAccessor)
        {

        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            //return base.GenerateClaimsAsync(user);
            var identity = await base.GenerateClaimsAsync(user).ConfigureAwait(false);
            identity.AddEnumClaim(Constants.ApplicationUserAccountTypeClaim, user.AccountType);
            return identity;
        }
    }
}
