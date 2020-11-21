using DeveloperCourse.SecondTask.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeveloperCourse.SecondTask.Identity.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<Domain.Entities.User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(x => x.Roles)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            builder.Metadata.FindNavigation(nameof(User.Roles))
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}