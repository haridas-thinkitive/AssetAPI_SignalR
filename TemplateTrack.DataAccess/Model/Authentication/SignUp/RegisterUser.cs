using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateTrack.DataAccess.Model.Authentication.SignUp
{
    public class RegisterUser
    {
        [Required(ErrorMessage ="User Name Requried")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Email Name Requried")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password Name Requried")]
        public string? Password { get; set; }


    }
}
