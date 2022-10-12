using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WindPowerWebApp.Model
{
    public class LoginModel
    {
        [Required, DisplayName("User Name")]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
