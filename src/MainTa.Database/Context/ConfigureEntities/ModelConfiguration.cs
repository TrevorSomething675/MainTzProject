using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MainTz.Database.Entities;

namespace MainTz.Database.Context.ConfigureEntities
{
    public class ModelConfiguration : IEntityTypeConfiguration<ModelEntity>
    {
        public void Configure(EntityTypeBuilder<ModelEntity> builder)
        {
            builder.HasMany(m => m.Cars)
                .WithOne(c => c.Model)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(c => c.ModelId)
                .IsRequired(false);

            builder.HasOne(m => m.Brand)
                .WithMany(b => b.Models)
                .HasForeignKey(m => m.BrandId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(true);
        }
    }
}
