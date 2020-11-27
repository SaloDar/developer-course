using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DeveloperCourse.SecondLesson.Common.Web.Attributes;
using DeveloperCourse.SecondLesson.Domain.Types;
using DeveloperCourse.SecondTask.Price.API.Controllers.DTOs;
using DeveloperCourse.SecondTask.Price.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DeveloperCourse.SecondTask.Price.API.Controllers
{
    [Authorize(Roles = nameof(UserRole.Administrator))]
    [ApiController]
    [Route("api/[controller]")]
    public class PricesController : ControllerBase
    {
        private readonly ILogger<PricesController> _logger;

        private readonly IMapper _mapper;

        private readonly IPriceService _priceService;

        public PricesController(ILogger<PricesController> logger, IMapper mapper, IPriceService priceService)
        {
            _logger = logger;
            _mapper = mapper;
            _priceService = priceService;
        }

        /// <summary>
        /// Retrieves all prices.
        /// </summary>
        /// <returns>Returns prices</returns>
        /// <response code="200">Returns prices</response>
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(GetPricesResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<GetPricesResponse> GetPrices([FromMultiSource] GetPricesRequest request)
        {
            var result = await _priceService.GetPrices(request?.ProductId, request?.Lasted);

            return _mapper.Map<GetPricesResponse>(result);
        }

        /// <summary>
        /// Retrieves a specific price by unique id.
        /// </summary>
        /// <returns>Returns the price</returns>
        /// <response code="200">Returns the price</response>
        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetPriceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<GetPriceResponse> GetPrice([FromMultiSource] GetPriceRequest request)
        {
            var result = await _priceService.GetPrice(request.Id);

            return _mapper.Map<GetPriceResponse>(result);
        }

        /// <summary>
        /// Creates a price.
        /// </summary>
        /// <returns>A newly created price</returns>
        /// <response code="200">Returns the newly created price</response>
        [HttpPost]
        [ProducesResponseType(typeof(CreatePriceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<CreatePriceResponse> CreatePrice(CreatePriceRequest request)
        {
            var result = await _priceService.CreatePrice(request.ProductId, request.RetailPrice, request.CostPrice, request.Currency);

            return _mapper.Map<CreatePriceResponse>(result);
        }
        
        /// <summary>
        /// Updated a specific price by unique id.
        /// </summary>
        /// <returns>Returns the updated price</returns>
        /// <response code="200">Returns the updated price</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(UpdatePriceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<UpdatePriceResponse> UpdatePrice([FromMultiSource] UpdatePriceRequest request)
        {
            var result = await _priceService.UpdatePrice(request.Id, request.Data?.ProductId, 
                request.Data?.RetailPrice, request.Data?.CostPrice, request.Data?.Currency);

            return _mapper.Map<UpdatePriceResponse>(result);
        }
        
        /// <summary>
        /// Deletes a specific price by unique id.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task DeletePrice([FromMultiSource] DeletePriceRequest request)
        {
            await _priceService.DeletePrice(request.Id);
        }
    }
}