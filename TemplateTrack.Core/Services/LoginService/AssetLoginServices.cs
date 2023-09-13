using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using TemplateTrack.Core.Data;
using TemplateTrack.Core.Interface.LoginAsset;
using TemplateTrack.DataAccess.Model;
using TemplateTrack.DataAccess.Model.LoginAssetUser;
using TemplateTrack.DataAccess.Model.Registration;

namespace TemplateTrack.Core.Services.LoginService
{
    public class AssetLoginServices : IAssetLogin
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public AssetLoginServices(ApplicationDbContext context , IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
        }


        public async  Task<List<RegistrationModel>> LoginAsset(string userName, string password)
        {
            var result = await _context.registrationModels.Where(u => u.Username == userName && u.Password == password).ToListAsync();
            return result;

        }

        public async  Task<string> LoginUser(string userName, string password)
        {

            try
            {

                if (userName != null && password !=null)
                {
                    var result = await _context.loginModels.Where(x => x.Username == userName && x.Password == password).ToListAsync();

                    if (result !=null && result.Count > 0)
                    {
                        var token = GenerateToken(userName, password);
                        //var token = GenerateJwtToken(result[0]);


                        return token ; 
                    }
                    return null; 
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private string GenerateToken(string UserName, string password)
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

        //private string GenerateJwtToken(LoginModel user)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes("your-secret-key"); // Replace with your secret key
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new[]
        //        {
        //        new Claim(ClaimTypes.Name, user.Password)
        //    }),
        //        Expires = DateTime.UtcNow.AddHours(1), // Token expiration time
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}
    }
}
