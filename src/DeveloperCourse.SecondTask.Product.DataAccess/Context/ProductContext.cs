using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using DeveloperCourse.SecondLesson.Domain.Entities;
using DeveloperCourse.SecondTask.Product.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeveloperCourse.SecondTask.Product.DataAccess.Context
{
    /// <example>
    /// Create Migration :
    /// dotnet ef migrations add 'MIGRATION_NAME' -p DeveloperCourse.SecondTask.Product.DataAccess -s DeveloperCourse.SecondTask.Product.API -v
    /// Remove migration, and revert the migration if it has been applied to the database :
    /// dotnet ef migrations remove -p DeveloperCourse.SecondTask.Product.DataAccess -s DeveloperCourse.SecondTask.Product.API -v -f
    /// Update Database :
    /// dotnet ef database update -p DeveloperCourse.SecondTask.Product.DataAccess -s DeveloperCourse.SecondTask.Product.API -v
    /// </example>
    public class ProductContext : DbContext, IProductContext
    {
        #region Entities

        public DbSet<Domain.Entities.Product> Products { get; set; }

        #endregion

        #region Constructors

        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
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

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.Changed();
                }
            }
        }

        #endregion
    }
}