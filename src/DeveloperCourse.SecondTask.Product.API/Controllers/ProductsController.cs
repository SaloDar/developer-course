using System.Threading.Tasks;
using AutoMapper;
using DeveloperCourse.SecondTask.Product.API.Controllers.DTOs;
using DeveloperCourse.SecondTask.Product.API.Interfaces;
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

        [HttpGet]
        public async Task<GetProductsResponse> GetProducts()
        {
            var result = await _productService.GetProducts();

            return _mapper.Map<GetProductsResponse>(result);
        }

        [HttpGet("{id}")]
        public async Task<GetProductResponse> GetProduct(GetProductRequest request)
        {
            var result = await _productService.GetProduct(request.Id);

            return _mapper.Map<GetProductResponse>(result);
        }
    }
}