using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MainTz.Database.Entities;

namespace MainTz.Database.Context.ConfigureEntities
{
    public class BrandConfiguration : IEntityTypeConfiguration<BrandEntity>
    {
        public void Configure(EntityTypeBuilder<BrandEntity> builder)
        {
            builder.HasMany(b => b.Models)
                .WithOne(m => m.Brand)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
        }
    }
}
