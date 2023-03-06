using SqlSugar;

namespace WindPowerWebApp.Model
{
    [SugarTable("Users")]
    public class UserModel
    {
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
    }
}
