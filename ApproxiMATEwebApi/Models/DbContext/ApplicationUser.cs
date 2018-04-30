using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApproxiMATEwebApi.Helpers;
using Microsoft.AspNetCore.Identity;

namespace ApproxiMATEwebApi.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        //public String DisplayName { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public AccountGender Gender { get; set; }
        public AccountType AccountType { get; set; }
        public DateTime? TermsAndConditionsDate { get; set; }

        //possibly abstract this to different table later if better performance is needed
        public double? CurrentLatitude { get; set; }
        public double? CurrentLongitude { get; set; }
        public DateTime? CurrentTimeStamp { get; set; }
    }
}
