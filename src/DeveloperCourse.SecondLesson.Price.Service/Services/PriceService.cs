using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DeveloperCourse.SecondLesson.Price.Service.DTOs;
using DeveloperCourse.SecondLesson.Price.Service.Interfaces;

namespace DeveloperCourse.SecondLesson.Price.Service.Services
{
    public class PriceService : IPriceService
    {
        private readonly IMemoryStore _memoryStore;

        private readonly IMapper _mapper;

        public PriceService(IMemoryStore memoryStore, IMapper mapper)
        {
            _memoryStore = memoryStore;
            _mapper = mapper;
        }

        public IEnumerable<PriceDto> GetAllPrices()
        {
            var prices = _memoryStore.Prices.ToList();

            return _mapper.Map<IEnumerable<PriceDto>>(prices).ToList();
        }

        public IEnumerable<PriceDto> GetProductPrices(Guid productId)
        {
            var prices = _memoryStore.Prices.Where(x => x.ProductId == productId).ToList();

            return _mapper.Map<IEnumerable<PriceDto>>(prices).ToList();
        }
    }
}