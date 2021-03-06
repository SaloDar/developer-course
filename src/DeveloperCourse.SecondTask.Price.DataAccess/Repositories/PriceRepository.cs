using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using DeveloperCourse.SecondLesson.Common.Identity.Interfaces;
using DeveloperCourse.SecondLesson.Domain.Types;
using DeveloperCourse.SecondTask.Infrastructure.Repositories;
using DeveloperCourse.SecondTask.Price.Domain.Interfaces;
using DeveloperCourse.SecondTask.Price.DataAccess.Configs;
using Microsoft.Extensions.Options;

namespace DeveloperCourse.SecondTask.Price.DataAccess.Repositories
{
    public class PriceRepository : BaseRepository<Domain.Entities.Price>, IPriceRepository
    {
        public PriceRepository(IOptions<PriceDbOptions> dbOptions, IUserContext userContext) : base(dbOptions, userContext, "price")
        {
        }

        public virtual async Task<IEnumerable<Domain.Entities.Price>> GetAll(bool lasted)
        {
            using var db = CreateConnection();

            return await db.QueryAsync<Domain.Entities.Price>($"SELECT * FROM [{TableName}] WHERE [IsDeleted] = 0 AND [IsLast] = @lasted", new {lasted});
        }
        
        public async Task<IEnumerable<Domain.Entities.Price>> GetPricesByProductId(Guid productId)
        {
            using var db = CreateConnection();

            return await db.QueryAsync<Domain.Entities.Price>($"SELECT * FROM [{TableName}] WHERE [ProductId] = @productId AND [IsDeleted] = 0", new {productId});
        }   
        
        public async Task<IEnumerable<Domain.Entities.Price>> GetPricesByProductId(Guid productId, bool lasted)
        {
            using var db = CreateConnection();

            return await db.QueryAsync<Domain.Entities.Price>($"SELECT * FROM [{TableName}] WHERE [ProductId] = @productId AND [IsLast] = @lasted AND [IsDeleted] = 0", new {productId,lasted});
        }
        
        public async Task<bool> UpdateIsLastByProduct(Guid productId, Currency currency)
        {
            using var db = CreateConnection();

            return await db.ExecuteAsync($"UPDATE [{TableName}] SET IsLast = 0 WHERE [ProductId] = @productId AND [Currency] = @currency AND [IsDeleted] = 0", 
                new {productId, currency}) != 0;
        }
    }
}