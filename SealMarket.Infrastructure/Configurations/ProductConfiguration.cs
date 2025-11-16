using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SealMarket.Core.Entities;

namespace SealMarket.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder
                .HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
