using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MainTz.Database.Entities;

namespace MainTz.Database.Context.ConfigureEntities
{
    public class ImageConfiguration : IEntityTypeConfiguration<ImageEntity>
    {
        public void Configure(EntityTypeBuilder<ImageEntity> builder)
        {
            builder.HasOne(img => img.Car)
                .WithMany(c => c.Images)
                .HasForeignKey(img => img.CarId)
                .IsRequired(true);
        }
    }
}