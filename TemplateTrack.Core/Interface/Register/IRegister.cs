using Microsoft.AspNetCore.Mvc;
using TemplateTrack.DataAccess.Model.Authentication.Login;
using TemplateTrack.DataAccess.Model.Authentication.SignUp;

namespace TemplateTrack.Core.Interface.Register
{
    public interface IRegister
    {
        Task<string> RegisterNewUser(RegisterUser registerUser, string role);

        Task<string> LoginUser(LoginModel loginModel);


    }
}