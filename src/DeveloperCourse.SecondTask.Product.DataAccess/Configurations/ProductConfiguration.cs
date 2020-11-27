using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeveloperCourse.SecondTask.Product.DataAccess.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Domain.Entities.Product>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Product> builder)
        {
            builder.ToTable("product");
            builder.HasKey(i => i.Id);
            builder.Property(c => c.Id).HasColumnName("id").HasColumnType("uuid");
            builder.Property(c => c.CreatedDate).HasColumnName("created_date");
            builder.Property(c => c.CreatedBy).HasColumnName("created_by").HasColumnType("uuid");
            builder.Property(c => c.LastSavedBy).HasColumnName("last_saved_by").HasColumnType("uuid");
            builder.Property(c => c.LastSavedDate).HasColumnName("last_saved_date");
            builder.Property(c => c.IsDeleted).HasColumnName("is_deleted");
            builder.Property(c => c.Name).HasColumnName("name").IsRequired();
            builder.Property(c => c.Description).HasColumnName("description").IsRequired();
            builder.Property(c => c.Sku).HasColumnName("sku").IsRequired();
            builder.Property(c => c.Weight).HasColumnName("weight").IsRequired();
            
            builder.HasQueryFilter(b => !b.IsDeleted);
        }
    }
}