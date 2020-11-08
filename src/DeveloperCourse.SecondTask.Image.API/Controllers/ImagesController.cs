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
    public class ImagesController : ControllerBase
    {
        private readonly ILogger<ImagesController> _logger;

        private readonly IMapper _mapper;

        private readonly IImageService _imageService;

        public ImagesController(ILogger<ImagesController> logger, IMapper mapper, IImageService imageService)
        {
            _logger = logger;
            _mapper = mapper;
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<GetImagesResponse> GetImages([FromMultiSource] GetImagesRequest request)
        {
            var result = await _imageService.GetImages(request?.ProductId);

            return _mapper.Map<GetImagesResponse>(result);
        }
        
        [HttpPost]
        public async Task<CreateProductImageResponse> CreateProductImage([FromForm] CreateProductImageRequest request)
        {
            var result = await _imageService.CreateImage(request.ProductId, request.File);

            return _mapper.Map<CreateProductImageResponse>(result);
        }

        [HttpGet("{id}")]
        public async Task<GetImageResponse> GetImage([FromMultiSource] GetImageRequest request)
        {
            var result = await _imageService.GetImage(request.Id);

            return _mapper.Map<GetImageResponse>(result);
        } 
        
        [HttpDelete("{id}")]
        public async Task DeleteImage([FromMultiSource] DeleteImageRequest request)
        {
            await _imageService.DeleteImage(request.Id);
        }
    }
}