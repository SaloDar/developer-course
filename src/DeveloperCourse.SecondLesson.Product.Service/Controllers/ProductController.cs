using System;
using System.Threading.Tasks;
using AutoMapper;
using DeveloperCourse.SecondLesson.Product.Service.Controllers.DTOs;
using DeveloperCourse.SecondLesson.Product.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DeveloperCourse.SecondLesson.Product.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        private readonly IMapper _mapper;

        private readonly IProductService _productService;
        
        public ProductController(ILogger<ProductController> logger, IMapper mapper, IProductService productService)
        {
            _logger = logger;
            _mapper = mapper;
            _productService = productService;
        }

        [HttpGet]
        public async Task<GetAllProductsResponse> GetAllProducts()
        {
            var result = await _productService.GetAllProducts();

            return _mapper.Map<GetAllProductsResponse>(result);
        }

        [HttpGet("{id}")]
        public async Task<GetProductResponse> GetProduct(Guid id)
        {
            var result = await _productService.GetProduct(id);

            return _mapper.Map<GetProductResponse>(result);
        }
    }
}