using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DeveloperCourse.SecondLesson.Image.Service.DTOs;
using DeveloperCourse.SecondLesson.Image.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeveloperCourse.SecondLesson.Image.Service.Services
{
    public class ImageService : IImageService
    {
        private readonly IMapper _mapper;

        private readonly IImageContext _imageContext;

        public ImageService(IMapper mapper, IImageContext imageContext)
        {
            _mapper = mapper;
            _imageContext = imageContext;
        }

        public async Task<IEnumerable<ImageDto>> GetAllImages()
        {
            var images = await _imageContext.Images.ToListAsync();

            return _mapper.Map<IEnumerable<ImageDto>>(images).ToList();
        }

        public async Task<IEnumerable<ImageDto>> GetProductImages(Guid productId)
        {
            var images = await _imageContext.Images.Where(x=>x.ProductId == productId).ToListAsync();

            return _mapper.Map<IEnumerable<ImageDto>>(images).ToList();
        }
    }
}