using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi
{
    public static class ClaimExtensions
    {
        public static void AddEnumClaim<T>(this ClaimsIdentity identity, String type, T enumValue) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("AddEnumClaim must be an enumeration");
            identity.AddClaim(new Claim(type, enumValue.ToString()));
        }
    }
}
