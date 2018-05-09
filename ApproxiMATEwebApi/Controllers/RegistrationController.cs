using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApproxiMATEwebApi.Models;
using ApproxiMATEwebApi.Services;
using ApproxiMATEwebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ApproxiMATEwebApi.Services.Interfaces;

namespace ApproxiMATEwebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Registration")]
    public class RegistrationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;

        public RegistrationController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync(UserRegister data)
        {
            var user = new ApplicationUser
            {
                UserName=data.UserName,
                Email=data.Email,
                DateOfBirth=data.DateofBirth,
                FirstName=data.FirstName,
                LastName=data.LastName,
                PhoneNumber=ExtractPhoneNumber.RemoveNonNumeric(data.PhoneNumber),
                AccountType=AccountType.Regular,
                Gender=data.Gender
            };
            var result = await _userManager.CreateAsync(user, data.Password);
            if(result.Succeeded)
            {
                _logger.LogInformation("New user created.");
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                await _emailSender.SendEmailConfirmationAsync(data.Email, callbackUrl);
                return Ok();
            }
            return BadRequest();
        }
    }
}