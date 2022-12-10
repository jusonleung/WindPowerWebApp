using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WindPowerWebApp.Model
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
