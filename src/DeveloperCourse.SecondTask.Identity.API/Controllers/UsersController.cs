using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DeveloperCourse.SecondTask.Identity.API.Controllers.DTOs;
using DeveloperCourse.SecondTask.Identity.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperCourse.SecondTask.Identity.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        private readonly IMapper _mapper;
        
        public UsersController(IAuthenticationService authenticationService, IMapper mapper)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get information about current user.
        /// </summary>
        /// <returns>Current user</returns>
        /// <response code="200">Returns current user</response>
        [ProducesResponseType(typeof(GetCurrentUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("me")]
        public async Task<GetCurrentUserResponse> GetCurrentUser()
        {
            var result = await _authenticationService.GetCurrentUser();

            return _mapper.Map<GetCurrentUserResponse>(result);
        }

        /// <summary>
        /// Register a new user.
        /// </summary>
        /// <returns>A newly created user</returns>
        /// <response code="200">Returns the newly created user</response>
        [ProducesResponseType(typeof(CreateUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<CreateUserResponse> CreateUser(CreateUserRequest request)
        {
            var result = await _authenticationService.Register(request.Username, request.Password);

            return _mapper.Map<CreateUserResponse>(result);
        }

        /// <summary>
        /// Authenticate user.
        /// </summary>
        /// <returns>Access token</returns>
        /// <response code="200">Returns the access token</response>
        [ProducesResponseType(typeof(CreateUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<AuthenticateUserResponse> AuthenticateUser(AuthenticateUserRequest request)
        {
            var result = await _authenticationService.Authenticate(request.Username, request.Password);

            return _mapper.Map<AuthenticateUserResponse>(result);
        }
    }
}