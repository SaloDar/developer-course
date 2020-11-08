using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeveloperCourse.SecondTask.Image.DataAccess.Configurations
{
    public class ImageConfiguration : IEntityTypeConfiguration<Domain.Entities.Image>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Image> builder)
        {
            builder.ToTable("image");
            builder.HasKey(i => i.Id);
            builder.Property(c => c.Id).HasColumnName("id").HasColumnType("uniqueidentifier");
            builder.Property(c => c.CreatedDate).HasColumnName("created_date");
            builder.Property(c => c.LastSavedDate).HasColumnName("last_saved_date");
            builder.Property(c => c.IsDeleted).HasColumnName("is_deleted");
            builder.Property(c => c.ProductId).HasColumnName("product_id").HasColumnType("uniqueidentifier");
            builder.Property(c => c.Link).HasColumnName("link");

            builder.HasQueryFilter(b => !b.IsDeleted);
        }
    }
}