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
        public static string CurrentUserId(this IHttpContextAccessor httpContextAccessor)
        {
            var stringId = httpContextAccessor?.HttpContext?.User?.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
            return stringId ?? String.Empty;
        }
    }
}
