using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using DeveloperCourse.SecondLesson.Image.Service.Infrastructure.Configs;
using DeveloperCourse.SecondLesson.Image.Service.Interfaces;
using DeveloperCourse.SecondLesson.Shared.Repositories;
using Microsoft.Extensions.Options;

namespace DeveloperCourse.SecondLesson.Image.Service.Repositories
{
    public class ImageRepository : BaseRepository<Entities.Image>, IImageRepository
    {
        public ImageRepository(IOptions<ImageDbOptions> dbOptions) : base(dbOptions, "image")
        {
        }

        public async Task<IEnumerable<Entities.Image>> GetImageByProductId(Guid productId)
        {
            using var db = CreateConnection();

            return await db.QueryAsync<Entities.Image>($"SELECT * FROM [{_tableName}] WHERE [ProductId] = @productId AND [IsDeleted] = 0", new {productId});
        }
    }
}