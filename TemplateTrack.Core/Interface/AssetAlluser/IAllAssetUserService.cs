using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateTrack.DataAccess.Model.Registration;

namespace TemplateTrack.Core.Interface.AssetAlluser
{
    public interface IAllAssetUserService
    {
         Task<List<RegistrationModel>> GetAllUser();

    }
}
