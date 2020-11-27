using System;
using System.Reflection;
using DeveloperCourse.SecondTask.Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DeveloperCourse.SecondTask.Identity.DataAccess.Context
{
    /// <example>
    /// Create Migration :
    /// dotnet ef migrations add 'MIGRATION_NAME' -p DeveloperCourse.SecondTask.Identity.DataAccess -s DeveloperCourse.SecondTask.Identity.API -v
    /// Remove migration, and revert the migration if it has been applied to the database :
    /// dotnet ef migrations remove -p DeveloperCourse.SecondTask.Identity.DataAccess -s DeveloperCourse.SecondTask.Identity.API -v -f
    /// Update Database :
    /// dotnet ef database update -p DeveloperCourse.SecondTask.Identity.DataAccess -s DeveloperCourse.SecondTask.Identity.API -v
    /// </example>
    public class IdentityContext : IdentityDbContext<User, Role, Guid, IdentityUserClaim<Guid>, RoleUser,
        IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        #region Constructors

        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
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

        #endregion
    }
}