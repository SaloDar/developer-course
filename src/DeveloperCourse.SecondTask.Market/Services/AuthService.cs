using System;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using DeveloperCourse.SecondLesson.Common.Clients.Clients.Identity;
using DeveloperCourse.SecondLesson.Common.Clients.Clients.Identity.DTOs;
using DeveloperCourse.SecondLesson.Common.Clients.Clients.Identity.DTOs.Requests;
using DeveloperCourse.SecondTask.Market.Interfaces;

namespace DeveloperCourse.SecondTask.Market.Services
{
    public class AuthService : IAuthService
    {
        private const string AuthTokenKey = "token";

        private readonly IIdentityClient _identityClient;

        private readonly ILocalStorageService _localStorage;

        public AuthService(IIdentityClient identityClient, ILocalStorageService localStorage)
        {
            _identityClient = identityClient;
            _localStorage = localStorage;
        }

        public async Task<bool> IsAuth()
        {
            return await GetCurrentUser() != null;
        }

        public async Task<bool> Login(string username, string password)
        {
            var request = new AuthenticateUserRequest
            {
                Username = username, Password = password
            };

            try
            {
                var response = await _identityClient.Authenticate(request);

                if (response == null)
                {
                    return false;
                }

                await _localStorage.SetItemAsync(AuthTokenKey, response.Token);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> Register(string username, string password)
        {
            var requestRegister = new RegisterUserRequest
            {
                Username = username, Password = password
            };

            try
            {
                var responseRegister = await _identityClient.Register(requestRegister);

                if (responseRegister?.User == null)
                {
                    return false;
                }

                var requestLogin = new AuthenticateUserRequest
                {
                    Username = username, Password = password
                };

                var responseLogin = await _identityClient.Authenticate(requestLogin);

                if (responseLogin == null)
                {
                    return false;
                }

                await _localStorage.SetItemAsync(AuthTokenKey, responseLogin.Token);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task Logout()
        {
            await _localStorage.SetItemAsync(AuthTokenKey, string.Empty);
        }

        public async Task<UserDto> GetCurrentUser()
        {
            try
            {
                var response = await _identityClient.GetCurrentUser();

                return response?.User;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> GetToken()
        {
            return await _localStorage.GetItemAsStringAsync(AuthTokenKey);
        }
    }
}