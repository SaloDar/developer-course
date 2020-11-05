using DeveloperCourse.SecondLesson.Product.Service.Infrastructure.Configs;
using DeveloperCourse.SecondLesson.Product.Service.Interfaces;
using DeveloperCourse.SecondLesson.Shared.Repositories;
using Microsoft.Extensions.Options;

namespace DeveloperCourse.SecondLesson.Product.Service.Repositories
{
    public class ProductRepository : BaseRepository<Entities.Product>, IProductRepository
    {
        public ProductRepository(IOptions<ProductDbOptions> dbOptions) : base(dbOptions, "product")
        {
        }
    }
}