using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DeveloperCourse.SecondLesson.Image.Service.DTOs;
using DeveloperCourse.SecondLesson.Image.Service.Interfaces;

namespace DeveloperCourse.SecondLesson.Image.Service.Services
{
    public class ImageService : IImageService
    {
        private readonly IMemoryStore _memoryStore;

        private readonly IMapper _mapper;

        public ImageService(IMemoryStore memoryStore, IMapper mapper)
        {
            _memoryStore = memoryStore;
            _mapper = mapper;
        }

        public IEnumerable<ImageDto> GetAllImages()
        {
            var images = _memoryStore.Images.ToList();

            return _mapper.Map<IEnumerable<ImageDto>>(images).ToList();
        }

        public IEnumerable<ImageDto> GetProductImages(Guid productId)
        {
            var images = _memoryStore.Images.Where(x => x.ProductId == productId).ToList();

            return _mapper.Map<IEnumerable<ImageDto>>(images).ToList();
        }
    }
}