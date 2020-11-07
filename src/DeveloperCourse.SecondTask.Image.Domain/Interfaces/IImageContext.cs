using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DeveloperCourse.SecondLesson.Image.Domain.Interfaces
{
    public interface IImageContext
    {
        #region Entities

        DbSet<Domain.Entities.Image> Images { get; set; }

        #endregion

        #region Methods

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        #endregion
    }
}