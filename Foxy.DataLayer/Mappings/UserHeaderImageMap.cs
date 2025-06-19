using Foxy.Core.Infrastructures.Constants;
using Foxy.DataLayer.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Foxy.DataLayer.Mappings;

public class UserHeaderImageMap : IEntityTypeConfiguration<UserHeaderImage>
{
    public void Configure(EntityTypeBuilder<UserHeaderImage> builder)
    {
        builder.HasKey(uhi => uhi.Id);

        builder.Property(uhi => uhi.Title)
            .IsRequired()
            .HasMaxLength(EntityConstants.GeneralTitleSize);

        builder.Property(uhi => uhi.ImageGuid)
            .HasMaxLength(EntityConstants.ImageGuidSize);

        builder.Property(uhi => uhi.RequiredLevel)
            .IsRequired();
    }
}