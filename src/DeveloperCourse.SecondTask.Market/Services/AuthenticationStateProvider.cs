using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DeveloperCourse.SecondTask.Market.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;

namespace DeveloperCourse.SecondTask.Market.Services
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthService _authService;

        public AuthStateProvider(IAuthService authService)
        {
            _authService = authService;
        }
        
        public void StateChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var result = await _authService.GetCurrentUser();

            if (result == null)
            {
                return new AuthenticationState(new ClaimsPrincipal());
            }

            var roleClaims = result.Roles
                .Select(x => new Claim(ClaimTypes.Role, x.ToString()))
                .ToList();

            var identity = new ClaimsIdentity(
                new List<Claim>(roleClaims)
                {
                    new Claim(ClaimTypes.Name, result.Username),
                    new Claim(ClaimTypes.NameIdentifier, result.Id.ToString())
                }, "Bearer", ClaimTypes.Name, ClaimTypes.Role);

            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }
    }
}