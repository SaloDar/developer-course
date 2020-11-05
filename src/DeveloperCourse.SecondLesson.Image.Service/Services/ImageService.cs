using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DeveloperCourse.SecondLesson.Image.Service.DTOs;
using DeveloperCourse.SecondLesson.Image.Service.Interfaces;

namespace DeveloperCourse.SecondLesson.Image.Service.Services
{
    public class ImageService : IImageService
    {
        private readonly IMapper _mapper;

        private readonly IImageRepository _imageRepository;

        public ImageService(IMapper mapper, IImageRepository imageRepository)
        {
            _mapper = mapper;
            _imageRepository = imageRepository;
        }

        public async Task<IEnumerable<ImageDto>> GetAllImages()
        {
            var images = await _imageRepository.GetAll();

            return _mapper.Map<IEnumerable<ImageDto>>(images).ToList();
        }

        public async Task<IEnumerable<ImageDto>> GetProductImages(Guid productId)
        {
            var images = await _imageRepository.GetImageByProductId(productId);

            return _mapper.Map<IEnumerable<ImageDto>>(images).ToList();
        }
    }
}