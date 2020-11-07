using System;
using System.Threading.Tasks;
using AutoMapper;
using DeveloperCourse.SecondTask.Image.API.Controllers.DTOs;
using DeveloperCourse.SecondTask.Image.API.Infrastructure.Attributes;
using DeveloperCourse.SecondTask.Image.API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DeveloperCourse.SecondTask.Image.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly ILogger<ImageController> _logger;

        private readonly IMapper _mapper;

        private readonly IImageService _imageService;
        
        public ImageController(ILogger<ImageController> logger, IMapper mapper, IImageService imageService)
        {
            _logger = logger;
            _mapper = mapper;
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<GetAllImagesResponse> GetAllImages()
        {
            var result = await _imageService.GetAllImages();

            return _mapper.Map<GetAllImagesResponse>(result);
        } 
        
        [HttpPost("product/{id}")]
        public async Task<CreateProductImageResponse> CreateProductImage([FromMultiSource] CreateProductImageRequest request)
        {
            var result = await _imageService.CreateProductImage(request.ProductId,request.Image);

            return _mapper.Map<CreateProductImageResponse>(result);
        }

        [HttpGet("product/{id}")]
        public async Task<GetProductImagesResponse> GetProductImages(Guid id)
        {
            var result = await _imageService.GetProductImages(id);

            return _mapper.Map<GetProductImagesResponse>(result);
        }
    }
}