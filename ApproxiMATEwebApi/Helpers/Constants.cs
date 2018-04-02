using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi
{
    public static class Constants
    {
        public static readonly String ApplicationUserAccountTypeClaim = "AccountType";
        public static readonly String JwtSecretNeedsToBeSecured = "the secret that needs to be at least 16 characeters long for HmacSha256";
        //public static readonly String HostedWebApiAddress = "https://localhost:44376";  //this should change? move to appsettings?
    }
}
