using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DeveloperCourse.SecondLesson.Product.Service.Clients;
using DeveloperCourse.SecondLesson.Product.Service.Clients.DTOs;
using DeveloperCourse.SecondLesson.Product.Service.DTOs;
using DeveloperCourse.SecondLesson.Product.Service.Interfaces;
using Microsoft.Extensions.Logging;

namespace DeveloperCourse.SecondLesson.Product.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;

        private readonly IImageClient _imageClient;

        private readonly IPriceClient _priceClient;

        private readonly IMapper _mapper;


        private readonly IProductRepository _productRepository;

        public ProductService(ILogger<ProductService> logger, IImageClient imageClient, IPriceClient priceClient, 
            IMapper mapper, IProductRepository productRepository)
        {
            _logger = logger;
            _imageClient = imageClient;
            _priceClient = priceClient;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            var products = await _productRepository.GetAll();

            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products).ToList();

            foreach (var product in productDtos)
            {
                var productImages = new GetProductImages
                {
                    Images = new List<ImageDto>()
                };

                var productPrices = new GetProductPrices
                {
                    Prices = new List<PriceDto>()
                };

                try
                {
                    productImages = await _imageClient.GetProductImages(product.Id);
                }
                catch (Exception)
                {
                    _logger.LogError($"Service {_imageClient.GetType()} api not available");
                }

                try
                {
                    productPrices = await _priceClient.GetProductImages(product.Id);
                }
                catch (Exception)
                {
                    _logger.LogError($"Service {_priceClient.GetType()} is not available");
                }

                product.Images = productImages.Images.ToList();
                product.Prices = productPrices.Prices.ToList();
            }

            return productDtos;
        }

        public async Task<ProductDto> GetProduct(Guid productId)
        {
            var product = await _productRepository.GetById(productId);

            if (product == null)
            {
                throw new Exception($"Product with id {productId} not found.");
            }

            var productDto = _mapper.Map<ProductDto>(product);

            var productImages = new GetProductImages
            {
                Images = new List<ImageDto>()
            };

            var productPrices = new GetProductPrices
            {
                Prices = new List<PriceDto>()
            };

            try
            {
                productImages = await _imageClient.GetProductImages(product.Id);
            }
            catch (Exception)
            {
                _logger.LogError($"Service {_imageClient.GetType()} api not available");
            }

            try
            {
                productPrices = await _priceClient.GetProductImages(product.Id);
            }
            catch (Exception)
            {
                _logger.LogError($"Service {_priceClient.GetType()} is not available");
            }

            productDto.Images = productImages.Images.ToList();
            productDto.Prices = productPrices.Prices.ToList();

            return productDto;
        }
    }
}