using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DeveloperCourse.SecondTask.Infrastructure.Attributes;
using DeveloperCourse.SecondTask.Product.API.Controllers.DTOs;
using DeveloperCourse.SecondTask.Product.API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DeveloperCourse.SecondTask.Product.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;

        private readonly IMapper _mapper;

        private readonly IProductService _productService;
        
        public ProductsController(ILogger<ProductsController> logger, IMapper mapper, IProductService productService)
        {
            _logger = logger;
            _mapper = mapper;
            _productService = productService;
        }

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>Returns products</returns>
        /// <response code="200">Returns products</response>
        [HttpGet]
        [ProducesResponseType(typeof(GetProductsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<GetProductsResponse> GetProducts()
        {
            var result = await _productService.GetProducts();

            return _mapper.Map<GetProductsResponse>(result);
        }

        /// <summary>
        /// Retrieves a specific product by unique id.
        /// </summary>
        /// <returns>Returns the product</returns>
        /// <response code="200">Returns the product</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<GetProductResponse> GetProduct([FromMultiSource] GetProductRequest request)
        {
            var result = await _productService.GetProduct(request.Id);

            return _mapper.Map<GetProductResponse>(result);
        }
    }
}