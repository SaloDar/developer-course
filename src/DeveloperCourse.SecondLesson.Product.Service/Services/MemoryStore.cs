using System;
using System.Collections.Generic;
using DeveloperCourse.SecondLesson.Product.Service.Interfaces;

namespace DeveloperCourse.SecondLesson.Product.Service.Services
{
    public class MemoryStore : IMemoryStore
    {
        public IEnumerable<Entities.Product> Products => new List<Entities.Product>
        {
            new Entities.Product
            {
                Id = new Guid("6766dc57-da6e-41ab-9f7e-746b5a99eb14"),
                Name = "Product #1",
                Description = "Description of product #1",
                Sku = "10000",
                Weight = "250 g.",
            },
            new Entities.Product
            {
                Id = new Guid("5e3bbe3a-3a71-4e2f-97cf-eea455ea18a8"),
                Name = "Product #2",
                Description = "Description of product #2",
                Sku = "20000",
                Weight = "50 g.",
            },
            new Entities.Product
            {
                Id = new Guid("249e3c38-6322-4c10-8792-55d82a7c4303"),
                Name = "Product #3",
                Description = "Description of product #3",
                Sku = "30000",
                Weight = "125 g.",
            },
            new Entities.Product
            {
                Id = new Guid("bad0468c-9ca9-4179-a20d-1f9eee74318b"),
                Name = "Product #4",
                Description = "Description of product #4",
                Sku = "40000",
                Weight = "100 g.",
            },
            new Entities.Product
            {
                Id = new Guid("0c9532bd-0f84-4672-b898-874ed82b7897"),
                Name = "Product #5",
                Description = "Description of product #5",
                Sku = "50000",
                Weight = "75 g.",
            }
        };
    }
}