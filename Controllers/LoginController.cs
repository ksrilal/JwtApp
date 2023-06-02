using JwtApp.Constants;
using JwtApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace JwtApp.Controllers
{
    public class LoginController : Controller
    {
        private IConfiguration _config;
        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserLoginModel userLoginModel)
        {
            var user = Authenticate(userLoginModel);
            
            if(user != null)
            {
                string token = GenerateToken(user);
                return Ok(token);
            }

            return NotFound("Invalid user credential");
        }

        private UserModel Authenticate(UserLoginModel userLogin)
        {
            var user = UserConstants.Users.FirstOrDefault(u => u.Username == userLogin.UserName && u.Password == userLogin.Password);
            return user;
        }

        private string GenerateToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Role,user.Role)
            };

            var token = new JwtSecurityToken( 
                    _config["Jwt:Issuer"],
                    _config["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(15),
                    signingCredentials: credentials );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
