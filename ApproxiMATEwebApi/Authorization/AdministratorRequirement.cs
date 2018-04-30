using ApproxiMATEwebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi.Authorization
{
    public class AdministratorRequirement : IAuthorizationRequirement
    {
        public AccountType AccountType { get; private set; }

        public AdministratorRequirement(AccountType accountType)
        {
            AccountType = accountType;
        }
    }
}
