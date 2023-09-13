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
using System.Threading.Tasks;
using TemplateTrack.Core.Data;
using TemplateTrack.Core.Interface.LoginAsset;
using TemplateTrack.DataAccess.Model;
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
    }
}
