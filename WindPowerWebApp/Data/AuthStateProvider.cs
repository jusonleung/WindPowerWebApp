using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text;
using WindPowerWebApp.Model;
using WindPowerWebApp.Service;
using System.Security.Cryptography;

namespace WindPowerWebApp.Data
{

    public class AuthStateProvider : AuthenticationStateProvider
    {

        private ILocalStorageService _localStorageService;
        private SqlDbService _sqlDbService;
        public AuthStateProvider(ILocalStorageService localStorageService, SqlDbService sqlDbService)
        {
            this._localStorageService = localStorageService;
            this._sqlDbService = sqlDbService;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string username = null;
            string password = null;

            try
            {
                username = await _localStorageService.GetItemAsync<string>("username");
                password = await _localStorageService.GetItemAsync<string>("password");
            }
            catch
            {
            }

            if (username == null || password == null)
            {
                _localStorageService.ClearAsync();
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            SHA256 SHA256 = SHA256.Create();
            var hahsPw = SHA256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var dbHashPw = _sqlDbService.GetPasswordHash(username);
            if (dbHashPw == null || !hahsPw.SequenceEqual(dbHashPw))
            {
                _localStorageService.ClearAsync();
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            else
            {
                var claims = new List<Claim>();

                claims.Add(new Claim(ClaimTypes.Name, username));

                var anonymous = new ClaimsIdentity(claims, "user");

                return new AuthenticationState(new ClaimsPrincipal(anonymous));
            }
        }
    }
}

