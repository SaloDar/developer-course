using DeveloperCourse.SecondTask.Infrastructure.Repositories;
using DeveloperCourse.SecondTask.Product.Domain.Interfaces;
using DeveloperCourse.SecondTask.Product.DataAccess.Configs;
using Microsoft.Extensions.Options;

namespace DeveloperCourse.SecondTask.Product.DataAccess.Repositories
{
    public class ProductRepository : BaseRepository<Domain.Entities.Product>, IProductRepository
    {
        public ProductRepository(IOptions<ProductDbOptions> dbOptions) : base(dbOptions, "product")
        {
        }
    }
}