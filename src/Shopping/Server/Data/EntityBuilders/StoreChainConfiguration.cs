using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.Shared.Data;

namespace Shopping.Server.Data.EntityBuilders
{
    public class StoreChainConfiguration : IEntityTypeConfiguration<StoreChain>
    {
        public void Configure(EntityTypeBuilder<StoreChain> builder)
        {
            builder.HasKey("StoreChainId");
            builder.Property(e => e.StoreChainId)
                .IsRequired();
            builder.Property(e => e.Name)
                .IsRequired();
            builder.Property(e => e.PriceCategory)
                .IsRequired();

            builder.HasMany<Store>(e => e.Stores)
                .WithOne(s => s.StoreChain)
                .HasForeignKey(s => s.StoreChainId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
        }
    }
}
