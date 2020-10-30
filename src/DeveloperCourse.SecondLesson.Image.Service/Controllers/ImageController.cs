using System;
using AutoMapper;
using DeveloperCourse.SecondLesson.Image.Service.Controllers.DTOs;
using DeveloperCourse.SecondLesson.Image.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DeveloperCourse.SecondLesson.Image.Service.Controllers
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
        public GetAllImagesResponse GetAllImages()
        {
            var result = _imageService.GetAllImages();

            return _mapper.Map<GetAllImagesResponse>(result);
        }

        [HttpGet("product/{id}")]
        public GetProductImagesResponse GetProductImages(Guid id)
        {
            var result = _imageService.GetProductImages(id);

            return _mapper.Map<GetProductImagesResponse>(result);
        }
    }
}