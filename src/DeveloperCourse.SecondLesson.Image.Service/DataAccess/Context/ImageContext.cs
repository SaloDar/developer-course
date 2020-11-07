using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using DeveloperCourse.SecondLesson.Image.Service.Interfaces;
using DeveloperCourse.SecondLesson.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeveloperCourse.SecondLesson.Image.Service.DataAccess.Context
{
    /// <example>
    /// Create Migration :
    /// dotnet ef migrations add 'MIGRATION_NAME' -p DeveloperCourse.SecondLesson.Image.Service -o 'DataAccess/Migrations' -v
    /// Remove migration, and revert the migration if it has been applied to the database :
    /// dotnet ef migrations remove -p DeveloperCourse.SecondLesson.Image.Service -v -f
    /// Update Database :
    /// dotnet ef database update -p  DeveloperCourse.SecondLesson.Image.Service -v
    /// </example>
    public class ImageContext : DbContext, IImageContext
    {
        #region Entities

        public DbSet<Entities.Image> Images { get; set; }

        #endregion

        #region Constructors

        public ImageContext(DbContextOptions<ImageContext> options) : base(options)
        {
        }

        #endregion

        #region Overridden Methodx

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override int SaveChanges()
        {
            OnBeforeSaving();

            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();

            return await base.SaveChangesAsync(cancellationToken);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        #endregion

        #region Private Methods

        private void OnBeforeSaving()
        {
            ChangeTracker.DetectChanges();

            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Entity.Deleted();
                }
            }
        }

        #endregion
    }
}