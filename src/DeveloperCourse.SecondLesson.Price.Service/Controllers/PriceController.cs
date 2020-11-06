using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DeveloperCourse.SecondLesson.Price.Service.Controllers.DTOs;
using DeveloperCourse.SecondLesson.Price.Service.Interfaces;
using DeveloperCourse.SecondLesson.Price.Service.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Money;

namespace DeveloperCourse.SecondLesson.Price.Service.Controllers
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

        [HttpGet("product/{id}")]
        public async Task<GetProductPricesResponse> GetProductImages(Guid id)
        {
            var result = await _priceService.GetProductPrices(id);

            return _mapper.Map<GetProductPricesResponse>(result);
        }
    }
}