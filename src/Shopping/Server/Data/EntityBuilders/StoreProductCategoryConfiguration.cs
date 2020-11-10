using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.Shared.Data;
using System;

namespace Shopping.Server.Data.EntityBuilders
{
    public class StoreProductCategoryConfiguration : IEntityTypeConfiguration<StoreProductCategory>
    {
        public void Configure(EntityTypeBuilder<StoreProductCategory> builder)
        {
            builder.HasKey(s => new { s.ProductCategoryId, s.StoreId });
            builder.HasOne(s => s.Store)
                .WithOne()
                .IsRequired();
            builder.HasOne(s => s.ProductCategory)
                .WithOne()
                .IsRequired();
            builder.Property(s => s.RankingValue)
                .IsRequired();

        }
    }
}
