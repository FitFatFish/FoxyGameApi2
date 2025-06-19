using Foxy.Core.Infrastructures.Constants;
using Foxy.DataLayer.Models.Games;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Foxy.DataLayer.Mappings;

public class GameCategoryMap : IEntityTypeConfiguration<GameCategory>
{
    public void Configure(EntityTypeBuilder<GameCategory> builder)
    {
        builder.HasKey(gc => gc.Id);

        builder.Property(gc => gc.Title)
            .IsRequired()
            .HasMaxLength(EntityConstants.GeneralTitleSize);

        builder.HasMany(gc => gc.Games)
            .WithOne(g => g.GameCategory)
            .HasForeignKey(g => g.GameCategoryId);
    }
}