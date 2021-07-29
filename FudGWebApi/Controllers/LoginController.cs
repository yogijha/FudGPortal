using FudGWebApi.Data;
using FudGWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FudGWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private readonly FudGPortalContext _context;

        public LoginController(IConfiguration config, FudGPortalContext context)
        {
            _config = config;
            _context = context;
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] LoginUser login)
        {
            IActionResult response = Unauthorized();
            var usera = AuthenticateUser(login);

            if (usera != null)
            {
                //var tokenString = GenerateJSONWebToken(usera)+" "+usera.role+","+usera.id;
                var tokenString = GenerateJSONWebToken(usera);
                usera.token = tokenString;
                response = Ok(usera);
                // response = Ok(new { token = tokenString });
            }
            return response;
        }

        private string GenerateJSONWebToken(LoginUser userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]//Claims...role based
            {

                new Claim(ClaimTypes.Role,userInfo.role)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
               claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private LoginUser AuthenticateUser(LoginUser login)
        {
            LoginUser user = null;
            var value = _context.Customers.Any(a => a.Emailid.Equals(login.email) & a.Password.Equals(login.password));
            var customer = _context.Customers.FirstOrDefault(a => a.Emailid.Equals(login.email) & a.Password.Equals(login.password));
            var Restaurant = _context.Restaurants.FirstOrDefault(a => a.Email.Equals(login.email) & a.Respassword.Equals(login.password));

            if (login.email.Equals("admin@gmail.com") && login.password.Equals("Admin"))
            {
                user = new LoginUser { email = login.email, password = login.password, role = "Admin", name = "Admin" };
            }
            else if (value)
            {
                {

                    user = new LoginUser { email = login.email, password = login.password, role = "Customer", id = customer.Customerid, name = customer.Customername };
                }
            }
            else if (Restaurant != null)
            {
                user = new LoginUser { email = login.email, password = login.password, role = "Restaurant", id = Restaurant.Resid, name = Restaurant.Resname };
            }
            return user;
        }
    }
}
