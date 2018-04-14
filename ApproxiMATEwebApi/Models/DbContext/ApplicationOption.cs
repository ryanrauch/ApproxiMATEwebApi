using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi.Models
{
    public class ApplicationOption
    {
        [Key]
        public int OptionsId { get; set; }
        public DateTime OptionsDate { get; set; }
        public string EndUserLicenseAgreementSource { get; set; }
        public string TermsConditionsSource { get; set; }
        public string PrivacyPolicySource { get; set; }
        public TimeSpan DataTimeWindow { get; set; }
    }
}
