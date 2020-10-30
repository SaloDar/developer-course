using System;
using System.Collections.Generic;
using DeveloperCourse.SecondLesson.Image.Service.Interfaces;

namespace DeveloperCourse.SecondLesson.Image.Service.Services
{
    public class MemoryStore : IMemoryStore
    {
        public IEnumerable<Entities.Image> Images => new List<Entities.Image>
        {
            new Entities.Image
            {
                Id = new Guid("15da8e4b-1e37-4d96-ad15-ae2625afa54a"),
                ProductId = new Guid("6766dc57-da6e-41ab-9f7e-746b5a99eb14"),
                Link = new Uri("https://i.imgur.com/jaSbTrr.jpg")
            },
            new Entities.Image
            {
                Id = new Guid("c1c9ac62-dd50-4224-b4e0-d05c5e6e619a"),
                ProductId = new Guid("6766dc57-da6e-41ab-9f7e-746b5a99eb14"),
                Link = new Uri("https://i.imgur.com/lXvRo3p.jpg")
            },
            new Entities.Image
            {
                Id = new Guid("4ca9dd3f-d543-4cf8-9560-fdca71ff19fc"),
                ProductId = new Guid("5e3bbe3a-3a71-4e2f-97cf-eea455ea18a8"),
                Link = new Uri("https://i.imgur.com/6NPVwAm.jpg")
            },
            new Entities.Image
            {
                Id = new Guid("b9b0c04f-3a38-4873-96c2-f9e0743749b9"),
                ProductId = new Guid("5e3bbe3a-3a71-4e2f-97cf-eea455ea18a8"),
                Link = new Uri("https://i.imgur.com/mEkyX7j.jpg")
            },
            new Entities.Image
            {
                Id = new Guid("6fba5a64-d25c-446e-a29c-6c751096ef6c"),
                ProductId = new Guid("249e3c38-6322-4c10-8792-55d82a7c4303"),
                Link = new Uri("https://i.imgur.com/GwTFdI8.jpg")
            },
            new Entities.Image
            {
                Id = new Guid("caa33960-e3bc-4d5f-8532-dde991329814"),
                ProductId = new Guid("249e3c38-6322-4c10-8792-55d82a7c4303"),
                Link = new Uri("https://i.imgur.com/KJc37AY.jpg")
            },
            new Entities.Image
            {
                Id = new Guid("6a7823b5-90f0-40d0-9820-8603058f5aa9"),
                ProductId = new Guid("bad0468c-9ca9-4179-a20d-1f9eee74318b"),
                Link = new Uri("https://i.imgur.com/fpRn70L.jpg")
            },
            new Entities.Image
            {
                Id = new Guid("a790c177-8c0c-4b64-b988-d32888b9f208"),
                ProductId = new Guid("bad0468c-9ca9-4179-a20d-1f9eee74318b"),
                Link = new Uri("https://i.imgur.com/mUpplO8.jpg")
            },
            new Entities.Image
            {
                Id = new Guid("5414471a-4fe8-410e-885e-a472ce51ede5"),
                ProductId = new Guid("0c9532bd-0f84-4672-b898-874ed82b7897"),
                Link = new Uri("https://i.imgur.com/uHbrTxe.jpg")
            },
            new Entities.Image
            {
                Id = new Guid("dec03f42-edc5-40cf-a943-c000e1318cb3"),
                ProductId = new Guid("0c9532bd-0f84-4672-b898-874ed82b7897"),
                Link = new Uri("https://i.imgur.com/NNtlhMj.jpg")
            },
        };
    }
}