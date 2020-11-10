using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shopping.Shared.Data;

namespace Shopping.Server.Data.EntityBuilders
{
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.HasKey("StoreId");
            builder.Property(e => e.StoreId)
                .IsRequired();
            builder.Property(e => e.Street)
                .IsRequired();
            builder.Property(e => e.HouseNumber)
                .IsRequired();
            builder.Property(e => e.PostalCode)
                .IsRequired();
            builder.Property(e => e.City)
                .IsRequired();
            builder.Property(e => e.PriceCategory)
                .IsRequired();
        }
    }
}
