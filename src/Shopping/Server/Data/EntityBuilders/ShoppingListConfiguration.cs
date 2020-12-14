using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.Shared.Data;
using System;

namespace Shopping.Server.Data.EntityBuilders
{
    public class ShoppingListConfiguration : IEntityTypeConfiguration<ShoppingList>
    {
        public void Configure(EntityTypeBuilder<ShoppingList> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.ListDate)
                .IsRequired();
            builder.Property(s => s.OwnerId)
                .IsRequired();
            builder.Ignore(s => s.Owner);
            builder.Ignore(s => s.UserGroups);
            builder.HasMany(s => s.Items)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Ignore(s => s.ItemCount);
            builder.Ignore(s => s.ListDone);
        }
    }
}
