using System;
using System.Linq;
using AutoMapper;
using DeveloperCourse.SecondLesson.Price.Service.Controllers.DTOs;
using DeveloperCourse.SecondLesson.Price.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        public GetAllPricesResponse GetAllPrices()
        {
            var result = _priceService.GetAllPrices().ToList();

            return _mapper.Map<GetAllPricesResponse>(result);
        }

        [HttpGet("product/{id}")]
        public GetProductPricesResponse GetProductImages(Guid id)
        {
            var result = _priceService.GetProductPrices(id).ToList();

            return _mapper.Map<GetProductPricesResponse>(result);
        }
    }
}