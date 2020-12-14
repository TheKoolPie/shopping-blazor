using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.Shared.Data;

namespace Shopping.Server.Data.EntityBuilders
{
    public class UserGroupMemberConfiguration : IEntityTypeConfiguration<UserGroupMembers>
    {
        public void Configure(EntityTypeBuilder<UserGroupMembers> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasOne(u => u.UserGroup)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(u => u.MemberId);
        }
    }
}
