using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TemplateTrack.Core.Data;
using TemplateTrack.DataAccess.Model;

namespace TemplateTrack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerateJWTController : ControllerBase
    {

        private readonly IConfiguration _config;

        public GenerateJWTController(IConfiguration configuration, ApplicationDbContext applicationDbContext)
        {
            _config = configuration;    

        }


        private User AuthenticateUser(User user)
        {
            User _user = null;
            if (user.Email == "Admin" && user.Password == "Hari@9763")
            {
                _user = new User { Email = "Haridas Dhulgande" };
            }

            return _user;
        }

        private string GenerateToken(User users)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                null,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            IActionResult responce = Unauthorized();
            var _user = AuthenticateUser(user);

            if (_user != null)
            {
                var token = GenerateToken(_user);
                responce = Ok(new { token });
            }
            return responce;
        }
    }
}
