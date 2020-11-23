using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DeveloperCourse.SecondLesson.Common.Clients.Clients.Image;
using DeveloperCourse.SecondLesson.Common.Clients.Clients.Price;
using DeveloperCourse.SecondLesson.Common.Identity.Interfaces;
using DeveloperCourse.SecondTask.Product.API.DTOs;
using DeveloperCourse.SecondTask.Product.API.Interfaces;
using DeveloperCourse.SecondTask.Product.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DeveloperCourse.SecondTask.Product.API.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;

        private readonly IImageClient _imageClient;

        private readonly IPriceClient _priceClient;

        private readonly IMapper _mapper;

        private readonly IProductContext _productContext;

        private readonly IUserContext _userContext;

        public ProductService(ILogger<ProductService> logger, IImageClient imageClient, IPriceClient priceClient,
            IMapper mapper, IProductContext productContext, IUserContext userContext)
        {
            _logger = logger;
            _imageClient = imageClient;
            _priceClient = priceClient;
            _mapper = mapper;
            _productContext = productContext;
            _userContext = userContext;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var products = await _productContext.Products.ToListAsync();

            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products).ToList();

            foreach (var product in productDtos)
            {
                product.Images = await GetProductImages(product.Id);

                product.Prices = await GetProductPrices(product.Id);
            }

            return productDtos;
        }

        public async Task<ProductDto> CreateProduct(string name, string description, string sku, string weight)
        {
            if (!_userContext.IsAuthenticated || _userContext?.Identity == null ||
                _userContext.Identity.UserId == Guid.Empty)
            {
                throw new Exception("Is not authenticated.");
            }

            var product = new Domain.Entities.Product(name, description, sku, weight, _userContext.Identity.UserId);

            await _productContext.Products.AddAsync(product);

            await _productContext.SaveChangesAsync();

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> GetProduct(Guid productId)
        {
            var product = await _productContext.Products.FirstOrDefaultAsync(x => x.Id == productId);

            if (product == null)
            {
                throw new Exception($"Product with id {productId} not found.");
            }

            var productDto = _mapper.Map<ProductDto>(product);

            productDto.Images = await GetProductImages(product.Id);

            productDto.Prices = await GetProductPrices(product.Id);

            return productDto;
        }

        public async Task DeleteProduct(Guid id)
        {
            var product = await _productContext.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                throw new Exception($"Product with id {id} was not found.");
            }

            _productContext.Products.Remove(product);

            await _productContext.SaveChangesAsync();
        }

        public async Task<ProductDto> UpdateProduct(Guid id, string name, string description, string sku, string weight)
        {
            if (id == Guid.Empty)
            {
                throw new InvalidOperationException("Product id can't be empty");
            }

            var product = await _productContext.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                throw new Exception($"Product with id {id} was not found.");
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                product.ChangeName(name);
            }

            if (!string.IsNullOrWhiteSpace(description))
            {
                product.ChangeDescription(description);
            }

            if (!string.IsNullOrWhiteSpace(sku))
            {
                product.ChangeSKU(sku);
            }

            if (!string.IsNullOrWhiteSpace(weight))
            {
                product.ChangeWeight(weight);
            }

            await _productContext.SaveChangesAsync();

            return _mapper.Map<ProductDto>(product);
        }

        private async Task<List<PriceDto>> GetProductPrices(Guid productId)
        {
            var result = new List<PriceDto>();

            try
            {
                var response = await _priceClient.GetPrices(productId);

                result = response.Prices?.ToList()
                    .Select(x => new PriceDto
                    {
                        Id = x.Id,
                        ProductId = x.ProductId,
                        Retail = x.Retail,
                        Currency = x.Currency,
                        IsLast = x.IsLast
                    })
                    .ToList() ?? new List<PriceDto>();
            }
            catch (Exception)
            {
                _logger.LogError($"Service {_priceClient.GetType()} is not available");
            }

            return result;
        }

        private async Task<List<ImageDto>> GetProductImages(Guid productId)
        {
            var result = new List<ImageDto>();

            try
            {
                var response = await _imageClient.GetImages(productId);

                result = response.Images?.Select(x=> new ImageDto
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    Link = x.Link
                }).ToList() ?? new List<ImageDto>();
            }
            catch (Exception)
            {
                _logger.LogError($"Service {_imageClient.GetType()} api not available");
            }

            return result;
        }
    }
}