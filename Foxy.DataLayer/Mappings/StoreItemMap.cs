using Foxy.Core.Infrastructures.Constants;
using Foxy.DataLayer.Models.Generals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Foxy.DataLayer.Mappings;

public class StoreItemMap : IEntityTypeConfiguration<StoreItem>
{
    public void Configure(EntityTypeBuilder<StoreItem> builder)
    {
        builder.HasKey(si => si.Id);

        builder.Property(si => si.Title)
            .IsRequired()
            .HasMaxLength(EntityConstants.GeneralTitleSize);
        
        builder.Property(si => si.ImageGuid)
            .HasMaxLength(EntityConstants.ImageGuidSize);

        builder.HasOne(si => si.Game)
            .WithMany()
            .HasForeignKey(si => si.GameId)
            .IsRequired(false);

        builder.HasMany(si => si.UserItems)
            .WithOne(ui => ui.storeItem)
            .HasForeignKey(ui => ui.StoreItemId);
    }
}