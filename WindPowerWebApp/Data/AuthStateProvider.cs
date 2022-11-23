using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using WindPowerWebApp.Model;

namespace WindPowerWebApp.Data
{

    public class AuthStateProvider : AuthenticationStateProvider
    {

        private ILocalStorageService localStorageService;
        public AuthStateProvider(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string user = null;
            try
            {
                user = await localStorageService.GetItemAsync<string>("webdatamodifier_user");
            }
            catch
            {

            }
            var admin = Admins.Where(a => a.Username == user).SingleOrDefault();
            if (admin == null)
            {
                var anonymous = new ClaimsIdentity();
                return new AuthenticationState(new ClaimsPrincipal(anonymous));
            }
            else
            {
                var claims = new List<Claim>();

                claims.Add(new Claim(ClaimTypes.Name, admin.Username));

                var anonymous = new ClaimsIdentity(claims, "customAuthType");

                return new AuthenticationState(new ClaimsPrincipal(anonymous));
            }
        }


        public List<LoginModel> Admins = new List<LoginModel>
            {
                new LoginModel
                {
                    Username = "admin",
                    Password = "admin"
                }
            };

        public LoginModel GetAdmin(string username, string password)
        {
            return Admins.Where(a => a.Username == username && a.Password == password).SingleOrDefault();
        }
    }
}

