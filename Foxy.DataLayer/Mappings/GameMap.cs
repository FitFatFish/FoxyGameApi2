using Foxy.Core.Infrastructures.Constants;
using Foxy.DataLayer.Models.Games;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Foxy.DataLayer.Mappings;

public class GameMap : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Title)
            .IsRequired()
            .HasMaxLength(EntityConstants.GeneralTitleSize);

        builder.Property(g => g.ImageGuid)
            .HasMaxLength(EntityConstants.ImageGuidSize);

        builder.Property(g => g.Documentation)
            .HasMaxLength(EntityConstants.DocumentTextSize);

        builder.HasOne(g => g.GameCategory)
            .WithMany(gc => gc.Games)
            .HasForeignKey(g => g.GameCategoryId);

        builder.HasMany(g => g.Matches)
            .WithOne(m => m.Game)
            .HasForeignKey(m => m.GameId);
    }
}