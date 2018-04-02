﻿using ApproxiMATEwebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public TokenController(
            SignInManager<ApplicationUser> signInManager,
            ILogger<TokenController> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Create(string username, string password, bool isPersistent)
        {
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
            //change to use identity provider??
            return !string.IsNullOrEmpty(username) && username == password;
        }

        private string GenerateToken(string username)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            };

            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.JwtSecretNeedsToBeSecured)),
                                             SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}