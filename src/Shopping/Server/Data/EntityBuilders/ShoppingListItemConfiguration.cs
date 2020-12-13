using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.Shared.Data;
using System;

namespace Shopping.Server.Data.EntityBuilders
{
    public class ShoppingListItemConfiguration : IEntityTypeConfiguration<ShoppingListItem>
    {
        public void Configure(EntityTypeBuilder<ShoppingListItem> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Amount)
                .IsRequired();
            builder.HasOne(p => p.ProductItem)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
