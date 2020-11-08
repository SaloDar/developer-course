using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DeveloperCourse.SecondTask.Image.API.Controllers.DTOs;
using DeveloperCourse.SecondTask.Image.API.Infrastructure.Attributes;
using DeveloperCourse.SecondTask.Image.API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

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

        /// <summary>
        /// Retrieves all images, or all images by product identifier.
        /// </summary>
        /// <returns>Returns images</returns>
        /// <response code="200">Returns images</response>
        [HttpGet]
        [ProducesResponseType(typeof(GetImagesResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<GetImagesResponse> GetImages([FromMultiSource] GetImagesRequest request)
        {
            var result = await _imageService.GetImages(request?.ProductId);

            return _mapper.Map<GetImagesResponse>(result);
        }
        
        /// <summary>
        /// Creates a image.
        /// </summary>
        /// <returns>A newly created image</returns>
        /// <response code="200">Returns the newly created image</response>
        [HttpPost]
        [ProducesResponseType(typeof(CreateProductImageResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<CreateProductImageResponse> CreateProductImage([FromForm] CreateProductImageRequest request)
        {
            var result = await _imageService.CreateImage(request.ProductId, request.File);

            return _mapper.Map<CreateProductImageResponse>(result);
        }

        /// <summary>
        /// Retrieves a specific image by unique id.
        /// </summary>
        /// <returns>Returns the image</returns>
        /// <response code="200">Returns the image</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetImageResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<GetImageResponse> GetImage([FromMultiSource] GetImageRequest request)
        {
            var result = await _imageService.GetImage(request.Id);

            return _mapper.Map<GetImageResponse>(result);
        } 
        
        /// <summary>
        /// Deletes a specific image by unique id.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task DeleteImage([FromMultiSource] DeleteImageRequest request)
        {
            await _imageService.DeleteImage(request.Id);
        }
    }
}