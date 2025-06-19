using Foxy.DataLayer.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Foxy.DataLayer.Mappings;

public class UserItemMap : IEntityTypeConfiguration<UserItem>
{
    public void Configure(EntityTypeBuilder<UserItem> builder)
    {
        builder.HasKey(ui => ui.Id);

        builder.HasOne(ui => ui.foxyUser)
            .WithMany(up => up.UserItems)
            .HasForeignKey(ui => ui.UserProfileId);

        builder.HasOne(ui => ui.storeItem)
            .WithMany(si => si.UserItems)
            .HasForeignKey(ui => ui.StoreItemId);
    }
}