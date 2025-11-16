using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SealMarket.Infrastructure.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(a => a.Id);

            builder
                .HasOne(a => a.Cart)
                .WithOne(c => c.Account)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(a => a.Notifications)
                .WithOne(n => n.Account)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
