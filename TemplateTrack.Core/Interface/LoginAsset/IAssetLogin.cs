using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateTrack.DataAccess.Model.Registration;

namespace TemplateTrack.Core.Interface.LoginAsset
{
    public interface IAssetLogin
    {
        Task<List<RegistrationModel>> LoginAsset(string userName, string password);

    }
}
