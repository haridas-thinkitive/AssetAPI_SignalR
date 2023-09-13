using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateTrack.Auth.Interface
{
    public interface IAuthenticationService
    {
        Task<IActionResult> Login([FromBody] LoginModel model);
    }
}
