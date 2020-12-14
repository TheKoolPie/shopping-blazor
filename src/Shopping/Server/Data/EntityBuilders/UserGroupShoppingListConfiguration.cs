using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.Shared.Data;


namespace Shopping.Server.Data.EntityBuilders
{
    public class UserGroupShoppingListConfiguration : IEntityTypeConfiguration<UserGroupShoppingList>
    {
        public void Configure(EntityTypeBuilder<UserGroupShoppingList> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasOne(u => u.UserGroup)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(u => u.ShoppingList)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
