using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using DeveloperCourse.SecondTask.Infrastructure.Repositories;
using DeveloperCourse.SecondTask.Price.Domain.Interfaces;
using DeveloperCourse.SecondTask.Price.DataAccess.Configs;
using Microsoft.Extensions.Options;

namespace DeveloperCourse.SecondTask.Price.DataAccess.Repositories
{
    public class PriceRepository : BaseRepository<Domain.Entities.Price>, IPriceRepository
    {
        public PriceRepository(IOptions<PriceDbOptions> dbOptions) : base(dbOptions, "price")
        {
        }

        public async Task<IEnumerable<Domain.Entities.Price>> GetPricesByProductId(Guid productId)
        {
            using var db = CreateConnection();

            return await db.QueryAsync<Domain.Entities.Price>($"SELECT * FROM [{TableName}] WHERE [ProductId] = @productId AND [IsDeleted] = 0", new {productId});
        }
    }
}