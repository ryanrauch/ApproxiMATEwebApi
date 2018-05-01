using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi.Extensions
{
    public static class IHttpContextAccessorExtensions
    {
        public static Guid CurrentUserGuid(this IHttpContextAccessor httpContextAccessor)
        {
            Guid gid = Guid.Empty;
            var stringId = httpContextAccessor?.HttpContext?.User?.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
            Guid.TryParse(stringId ?? "0", out gid);
            return gid;
        }
    }
}
