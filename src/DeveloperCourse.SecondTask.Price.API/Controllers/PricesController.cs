using System.Threading.Tasks;
using AutoMapper;
using DeveloperCourse.SecondTask.Infrastructure.Attributes;
using DeveloperCourse.SecondTask.Price.API.Controllers.DTOs;
using DeveloperCourse.SecondTask.Price.API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DeveloperCourse.SecondTask.Price.API.Controllers
{
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

        [HttpGet]
        public async Task<GetPricesResponse> GetPrices([FromMultiSource] GetPricesRequest request)
        {
            var result = await _priceService.GetPrices(request?.ProductId, request.Lasted);

            return _mapper.Map<GetPricesResponse>(result);
        }

        [HttpGet("{id}")]
        public async Task<GetPriceResponse> GetPrice([FromMultiSource] GetPriceRequest request)
        {
            var result = await _priceService.GetPrice(request.Id);

            return _mapper.Map<GetPriceResponse>(result);
        }

        [HttpPost]
        public async Task<CreatePriceResponse> CreatePrice(CreatePriceRequest request)
        {
            var result = await _priceService.CreatePrice(request.ProductId, request.RetailPrice, request.CostPrice, request.Currency);

            return _mapper.Map<CreatePriceResponse>(result);
        }
        
        [HttpPatch("{id}")]
        public async Task<UpdatePriceResponse> UpdatePrice([FromMultiSource] UpdatePriceRequest request)
        {
            var result = await _priceService.UpdatePrice(request.Id, request.Data?.ProductId, 
                request.Data?.RetailPrice, request.Data?.CostPrice, request.Data?.Currency);

            return _mapper.Map<UpdatePriceResponse>(result);
        }
        
        [HttpDelete("{id}")]
        public async Task DeletePrice([FromMultiSource] DeletePriceRequest request)
        {
            await _priceService.DeletePrice(request.Id);
        }
    }
}