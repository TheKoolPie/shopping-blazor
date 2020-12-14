using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.Shared.Data;
using System;

namespace Shopping.Server.Data.EntityBuilders
{
    public class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
    {
        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Name)
                .IsRequired();
            builder.Property(u => u.OwnerId)
                .IsRequired();
            builder.Ignore(u => u.Owner);
            builder.Ignore(u => u.ShoppingLists);
            builder.Ignore(u => u.Members);
        }
    }
}
