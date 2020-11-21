using DeveloperCourse.SecondTask.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeveloperCourse.SecondTask.Identity.DataAccess.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Domain.Entities.Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(x => x.Value);
        }
    }
}