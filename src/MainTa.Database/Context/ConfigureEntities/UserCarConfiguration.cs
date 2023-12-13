using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MainTz.Database.Entities;

namespace MainTz.Database.Context.ConfigureEntities
{
    public class UserCarConfiguration : IEntityTypeConfiguration<UserCarEntity>
    {
        public void Configure(EntityTypeBuilder<UserCarEntity> builder)
        {
            builder.HasKey(uc => uc.UserId);
            builder.HasKey(uc => uc.CarId);

            builder.HasOne(uc => uc.User)
                .WithMany(u => u.Cars)
                .IsRequired(true);

            builder.HasOne(uc => uc.Car)
                .WithMany(c => c.Users)
                .IsRequired(true);
        }
    }
}