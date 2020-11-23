using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DeveloperCourse.SecondLesson.Common.Identity.Configs;
using DeveloperCourse.SecondLesson.Common.Identity.Interfaces;
using DeveloperCourse.SecondLesson.Domain.Types;
using DeveloperCourse.SecondTask.Identity.API.DTOs;
using DeveloperCourse.SecondTask.Identity.API.Interfaces;
using DeveloperCourse.SecondTask.Identity.Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DeveloperCourse.SecondTask.Identity.API.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields

        private readonly ILogger<AuthenticationService> _logger;

        private readonly IUserManagerService _userManagerService;

        private readonly IRoleManager _roleManager;

        private readonly IMapper _mapper;

        private readonly SecurityConfig _securityConfig;

        private readonly IUserContext _userContext;

        #endregion

        #region Constructors

        public AuthenticationService(ILogger<AuthenticationService> logger, IUserManagerService userManagerService,
            IRoleManager roleManager, IMapper mapper, IOptions<SecurityConfig> securityConfig, IUserContext userContext)
        {
            _logger = logger;
            _userManagerService = userManagerService;
            _roleManager = roleManager;
            _mapper = mapper;
            _securityConfig = securityConfig.Value;
            _userContext = userContext;
        }

        #endregion

        #region Implemented Methods

        public async Task<UserDto> Register(string username, string password)
        {
            var userExist = await _userManagerService.FindByNameAsync(username);

            if (userExist != null)
            {
                throw new InvalidOperationException("User with same username exist");
            }

            var user = new User(username);

            var result = await _userManagerService.CreateAsync(user, password);

            if (!result)
            {
                throw new Exception("An error occurred while creating the user");
            }

            if (!await _roleManager.RoleExistsAsync(UserRole.User))
            {
                await _roleManager.CreateAsync(new Role(UserRole.User));
            }

            await _userManagerService.AddToRoleAsync(user, UserRole.User.ToString());

            return _mapper.Map<UserDto>(user);
        }

        public async Task<string> Authenticate(string username, string password)
        {
            var user = await _userManagerService.FindByNameAsync(username);

            if (user == null || !await _userManagerService.CheckPasswordAsync(user, password))
            {
                throw new InvalidOperationException("User not found or not correct password");
            }

            var userRoles = await _userManagerService.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var rolesClaims = userRoles
                .Select(x => new Claim(ClaimTypes.Role, x))
                .ToList();

            claims.AddRange(rolesClaims);

            var audiencesClaims = _securityConfig.Audiences
                .Select(x => new Claim(JwtRegisteredClaimNames.Aud, x))
                .ToList();

            claims.AddRange(audiencesClaims);

            var signingKey = new SymmetricSecurityKey(_securityConfig.SigningKeyBytes);
            var encryptingKey = new SymmetricSecurityKey(_securityConfig.EncryptionKeyBytes);
            var credentials = new SigningCredentials(signingKey, _securityConfig.SigningKeyAlgorithm);

            var encryptingCredentials = new EncryptingCredentials(encryptingKey,
                JwtConstants.DirectKeyUseAlg, _securityConfig.EncryptionKeyAlgorithm);

            var expires = DateTime.UtcNow.Add(_securityConfig.ExpirationTime);

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateJwtSecurityToken(
                _securityConfig.Issuer,
                null,
                new ClaimsIdentity(claims),
                DateTime.UtcNow,
                expires,
                DateTime.UtcNow,
                credentials,
                encryptingCredentials
            );

            var encryptedToken = tokenHandler.WriteToken(securityToken);

            return encryptedToken;
        }

        public async Task<UserDto> GetCurrentUser()
        {
            if (!_userContext.IsAuthenticated)
            {
                throw new Exception("Is not authenticated.");
            }

            if (_userContext.Identity.UserId == Guid.Empty)
            {
                throw new Exception("Is not authenticated.");
            }

            var user = await _userManagerService.FindByIdAsync(_userContext.Identity.UserId);

            if (user == null)
            {
                throw new Exception($"Cannot find user with id {_userContext.Identity.UserId}");
            }

            return _mapper.Map<UserDto>(user);
        }

        #endregion
    }
}