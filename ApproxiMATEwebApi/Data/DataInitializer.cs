using ApproxiMATEwebApi.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi.Data
{
    public class DataInitializer
    {
        ApplicationDbContext _context { get; set; }

        public async Task InitializeDataAsync(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            // Add the code for inintializing at here
            if (_context.ApplicationOptions.Count() == 0)
            {
                _context.ApplicationOptions.Add(new ApplicationOption()
                {
                    OptionsDate = DateTime.Now,
                    EndUserLicenseAgreementSource = "https://www.lipsum.com/",
                    PrivacyPolicySource = "https://www.lipsum.com/",
                    TermsConditionsSource = "https://www.lipsum.com/"
                });
                await _context.SaveChangesAsync();
            }
        }
    }
}
