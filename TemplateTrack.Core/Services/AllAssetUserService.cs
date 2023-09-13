using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateTrack.Core.Data;
using TemplateTrack.Core.Interface.AssetAlluser;
using TemplateTrack.DataAccess.Model.Registration;

namespace TemplateTrack.Core.Services
{
    public class AllAssetUserService : IAllAssetUserService
    {
        private readonly ApplicationDbContext _context;


        public AllAssetUserService(ApplicationDbContext context )
        {
            _context = context;
        }

       public async Task<List<RegistrationModel>> GetAllUser()
        {

            var result = await _context.registrationModels.ToListAsync();
            return result;

        }

    }
}
