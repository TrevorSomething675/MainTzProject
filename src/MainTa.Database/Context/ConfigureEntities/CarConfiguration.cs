using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MainTz.Database.Entities;

namespace MainTz.Database.Context.ConfigureEntities
{
    public class CarConfiguration : IEntityTypeConfiguration<CarEntity>
    {
        public void Configure(EntityTypeBuilder<CarEntity> builder)
        {
            builder.HasMany(c => c.Users)
                .WithOne(u => u.Car)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(uc => uc.CarId)
                .IsRequired(false);

            builder.HasMany(c => c.Images)
                .WithOne(img => img.Car)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(img => img.CarId)
                .IsRequired(false);

            builder.HasOne(c => c.Model)
                .WithMany(m => m.Cars)
                .HasForeignKey(c => c.ModelId)
                .IsRequired(true);
        }
    }
}