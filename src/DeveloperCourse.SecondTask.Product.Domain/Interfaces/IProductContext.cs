using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DeveloperCourse.SecondTask.Product.Domain.Interfaces
{
    public interface IProductContext
    {
        #region Entities

        DbSet<Domain.Entities.Product> Products { get; set; }

        #endregion

        #region Methods

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        #endregion
    }
}