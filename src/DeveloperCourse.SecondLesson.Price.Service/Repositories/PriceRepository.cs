using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using DeveloperCourse.SecondLesson.Price.Service.Infrastructure.Configs;
using DeveloperCourse.SecondLesson.Price.Service.Interfaces;
using DeveloperCourse.SecondLesson.Shared.Repositories;
using Microsoft.Extensions.Options;

namespace DeveloperCourse.SecondLesson.Price.Service.Repositories
{
    public class PriceRepository : BaseRepository<Entities.Price>, IPriceRepository
    {
        public PriceRepository(IOptions<PriceDbOptions> dbOptions) : base(dbOptions, "price")
        {
        }

        public async Task<IEnumerable<Entities.Price>> GetPricesByProductId(Guid productId)
        {
            using var db = CreateConnection();

            return await db.QueryAsync<Entities.Price>($"SELECT * FROM [{_tableName}] WHERE [ProductId] = @productId AND [IsDeleted] = 0", new {productId});
        }
    }
}