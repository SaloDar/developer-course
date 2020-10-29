using System;
using System.Collections.Generic;
using DeveloperCourse.SecondLesson.Price.Service.Interfaces;
using Money;

namespace DeveloperCourse.SecondLesson.Price.Service.Services
{
    public class MemoryStore : IMemoryStore
    {
        public IEnumerable<Entities.Price> Prices => new List<Entities.Price>
        {
            new Entities.Price
            {
                Id = new Guid("98edebd9-94e1-4fd6-9d4d-cdf3afb00672"),
                ProductId = new Guid("6766dc57-da6e-41ab-9f7e-746b5a99eb14"),
                Amount = 10,
                Currency = Currency.USD
            },
            new Entities.Price
            {
                Id = new Guid("6c9a9a67-d898-47af-a883-15bc54250a70"),
                ProductId = new Guid("6766dc57-da6e-41ab-9f7e-746b5a99eb14"),
                Amount = 700,
                Currency = Currency.RUB
            },
            new Entities.Price
            {
                Id = new Guid("37e13689-26c0-4ec3-ab5b-c17d1264cdfa"),
                ProductId = new Guid("5e3bbe3a-3a71-4e2f-97cf-eea455ea18a8"),
                Amount = 2,
                Currency = Currency.USD
            },
            new Entities.Price
            {
                Id = new Guid("2c32e504-9d4e-4070-9c64-62293ce97b52"),
                ProductId = new Guid("5e3bbe3a-3a71-4e2f-97cf-eea455ea18a8"),
                Amount = 150,
                Currency = Currency.RUB
            },
            new Entities.Price
            {
                Id = new Guid("d456f36e-5d37-48ad-bbdc-63b7f2f7fa3c"),
                ProductId = new Guid("249e3c38-6322-4c10-8792-55d82a7c4303"),
                Amount = 5,
                Currency = Currency.USD
            },
            new Entities.Price
            {
                Id = new Guid("edf74d12-b496-43c3-825b-77e21957cc2b"),
                ProductId = new Guid("249e3c38-6322-4c10-8792-55d82a7c4303"),
                Amount = 400,
                Currency = Currency.RUB
            },
            new Entities.Price
            {
                Id = new Guid("993db5e6-e5fe-47b5-b950-d9f2e58139af"),
                ProductId = new Guid("bad0468c-9ca9-4179-a20d-1f9eee74318b"),
                Amount = 7,
                Currency = Currency.USD
            },
            new Entities.Price
            {
                Id = new Guid("212d882c-bf54-4882-83bc-14c9aa5d65b8"),
                ProductId = new Guid("bad0468c-9ca9-4179-a20d-1f9eee74318b"),
                Amount = 550,
                Currency = Currency.RUB
            },
            new Entities.Price
            {
                Id = new Guid("bc50e8cc-5c02-463e-a8f7-d712abb856a3"),
                ProductId = new Guid("0c9532bd-0f84-4672-b898-874ed82b7897"),
                Amount = 1,
                Currency = Currency.USD
            },
            new Entities.Price
            {
                Id = new Guid("788917cc-82ee-4b67-8663-5691865eca80"),
                ProductId = new Guid("0c9532bd-0f84-4672-b898-874ed82b7897"),
                Amount = 100,
                Currency = Currency.RUB
            }
        };
    }
}