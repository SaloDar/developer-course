using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DeveloperCourse.SecondTask.Price.API.DTOs;
using DeveloperCourse.SecondTask.Price.API.Interfaces;
using DeveloperCourse.SecondTask.Price.Domain.Interfaces;
using Money;

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

        public async Task<PriceDto> GetPrice(Guid id)
        {
            var price = await _priceRepository.GetById(id);
            
            if (price == null)
            {
                throw new Exception($"Price with id {id} was not found.");
            }

            return _mapper.Map<PriceDto>(price);
        }

        public async Task DeletePrice(Guid id)
        {
            var result =  await _priceRepository.Delete(id);

            if (!result)
            {
                throw new Exception($"Couldn't delete price with id {id}.");
            }
        }

        public async Task<IEnumerable<PriceDto>> GetPrices(Guid? productId = null)
        {
            List<Domain.Entities.Price> prices;
            
            if (productId.HasValue && productId.Value != Guid.Empty)
            {
                var productPrices = await _priceRepository.GetPricesByProductId(productId.Value);

                prices = productPrices?.ToList() ?? new List<Domain.Entities.Price>();

            }
            else
            {
                var allPrices = await _priceRepository.GetAll();
                
                prices = allPrices?.ToList() ?? new List<Domain.Entities.Price>();
            }
            
            return _mapper.Map<IEnumerable<PriceDto>>(prices).ToList();
        }

        public async Task<PriceDto> CreatePrice(Guid productId, decimal retailPrice, decimal costPrice, Currency currency)
        {
            await _priceRepository.UpdateIsLastByProduct(productId, currency);

            var newPrice = await _priceRepository.Create(new Domain.Entities.Price(productId, retailPrice, costPrice, currency));

            return _mapper.Map<PriceDto>(newPrice);
        }
    }
}