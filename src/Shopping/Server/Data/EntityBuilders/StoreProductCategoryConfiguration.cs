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
            builder.HasKey(s => s.StoreProductCategoryId);
            builder.HasOne(s => s.Store)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(s => s.ProductCategory)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(s => s.RankingValue)
                .IsRequired();

        }
    }
}
