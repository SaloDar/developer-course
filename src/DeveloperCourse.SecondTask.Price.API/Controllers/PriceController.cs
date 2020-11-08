using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DeveloperCourse.SecondTask.Price.API.Controllers.DTOs;
using DeveloperCourse.SecondTask.Price.API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Money;

namespace DeveloperCourse.SecondTask.Price.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PriceController : ControllerBase
    {
        private readonly ILogger<PriceController> _logger;

        private readonly IMapper _mapper;

        private readonly IPriceService _priceService;
        
        public PriceController(ILogger<PriceController> logger, IMapper mapper, IPriceService priceService)
        {
            _logger = logger;
            _mapper = mapper;
            _priceService = priceService;
        }

        [HttpGet]
        public async Task<GetAllPricesResponse> GetAllPrices()
        {
            var result = await _priceService.GetAllPrices();

            return _mapper.Map<GetAllPricesResponse>(result);
        }
        
        [HttpPost]
        public async Task<CreatePriceResponse> CreatePrice(CreatePriceRequest request)
        {
            var result = await _priceService.CreatePrice(request.ProductId, request.RetailPrice, 
                request.CostPrice, request.Currency);

            return _mapper.Map<CreatePriceResponse>(result);
        }

        [HttpGet("product/{id}")]
        public async Task<GetProductPricesResponse> GetProductImages(Guid id)
        {
            var result = await _priceService.GetProductPrices(id);

            return _mapper.Map<GetProductPricesResponse>(result);
        }
    }
}