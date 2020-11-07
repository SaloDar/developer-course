using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DeveloperCourse.SecondTask.Price.API.DTOs;
using DeveloperCourse.SecondTask.Price.API.Interfaces;
using DeveloperCourse.SecondTask.Price.Domain.Interfaces;

namespace DeveloperCourse.SecondTask.Price.API.Services
{
    public class PriceService : IPriceService
    {
        private readonly IMapper _mapper;

        private readonly IPriceRepository _priceRepository;

        public PriceService(IMapper mapper, IPriceRepository priceRepository)
        {
            _mapper = mapper;
            _priceRepository = priceRepository;
        }

        public async Task<IEnumerable<PriceDto>> GetAllPrices()
        {
            var prices = await _priceRepository.GetAll();

            return _mapper.Map<IEnumerable<PriceDto>>(prices).ToList();
        }

        public async Task<IEnumerable<PriceDto>> GetProductPrices(Guid productId)
        {
            var prices = await _priceRepository.GetPricesByProductId(productId);

            return _mapper.Map<IEnumerable<PriceDto>>(prices).ToList();
        }
    }
}