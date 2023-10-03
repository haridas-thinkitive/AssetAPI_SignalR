using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web.Mvc;
using TemplateTrack.Core.Interface.Register;
using TemplateTrack.DataAccess.Model;
using TemplateTrack.DataAccess.Model.Authentication.Login;
using TemplateTrack.DataAccess.Model.Authentication.SignUp;
using TemplateTrack.DataAccess.Model.BatchAsset;
using TemplateTrack.DataAccess.Model.Responce;
using static GoogleMaps.LocationServices.Directions;

namespace TemplateTrack.Core.Services.RegistrationService
{
    public class UserRegistrationService : IRegister
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public UserRegistrationService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;

        }

        public async Task<string> RegisterNewUser(RegisterUser registerUser,string Role)
        {
            var userExist = await _userManager.FindByNameAsync(registerUser.UserName);
            if (userExist != null)
            {
                return "User Already Exist";
            }
            IdentityUser user = new()
            {
                UserName = registerUser.UserName,
                Email = registerUser.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            if(await _roleManager.RoleExistsAsync(Role))
            {
                var result = await _userManager.CreateAsync(user, registerUser.Password);
                if (!result.Succeeded)
                {
                    return "User Failed To Create";
                }
                //Add role 
                await _userManager.AddToRoleAsync(user,Role);
                return "User Created Successfully";
            }
            else
            {
                return "This Role Dosen't Exist";
            }

            
        }

        public async Task<string> LoginUser(LoginModel loginModel)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(loginModel.UserName);
                if (user != null && await _userManager.CheckPasswordAsync(user,loginModel.Password))
                {
                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    };
                    var userRoles = await _userManager.GetRolesAsync(user);
                    foreach (var role in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, role));
                    }
                    var jwtToken = GetJwtToken(authClaims);
                    return jwtToken.ToString();
                }
                return "Invalid User";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetJwtToken(List<Claim> authClaims)
        {
            var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: authClaims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}
