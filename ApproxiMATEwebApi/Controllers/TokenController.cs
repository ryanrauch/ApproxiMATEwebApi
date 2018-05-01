using ApproxiMATEwebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi.Controllers
{
    [Route("/token")]
    public class TokenController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public TokenController(
            SignInManager<ApplicationUser> signInManager,
            ILogger<TokenController> logger,
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _logger = logger;
            _configuration = configuration;
            _userManager = userManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Create(string username, string password, bool isPersistent)
        {
            //TODO: re-write this entire controller
            //      to be async, and implement better security measures
            if (IsValidUserAndPasswordCombination(username, password, isPersistent))
                return new ObjectResult(GenerateToken(username));
            return BadRequest();
        }

        private bool IsValidUserAndPasswordCombination(string username, string password, bool isPersistent)
        {
            var result = _signInManager.PasswordSignInAsync(username, password, isPersistent, false).Result;
            if(result == Microsoft.AspNetCore.Identity.SignInResult.Success)
            {
                _logger.LogInformation("User logged in.");
                return true;
            }
            //WTF is this??
            //return !string.IsNullOrEmpty(username) && username == password;
            return false;
        }

        private string GenerateToken(string username)
        {
            var appUser = _userManager.FindByNameAsync(username).Result;
            var claims = new Claim[]
            {
                //new Claim(ClaimTypes.Name, username),
                //new Claim(ClaimTypes.NameIdentifier, appUser.Id),
                new Claim(JwtRegisteredClaimNames.Jti, appUser.Id),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            };

            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[Constants.JwtSecretKey])),
                                             SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}