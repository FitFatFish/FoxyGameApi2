using Foxy.DataLayer.Models.Games;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Foxy.DataLayer.Mappings;

public class MatchMap : IEntityTypeConfiguration<Match>
{
    public void Configure(EntityTypeBuilder<Match> builder)
    {
        builder.HasKey(m => m.Id);

        builder.HasOne(m => m.Game)
            .WithMany(g => g.Matches)
            .HasForeignKey(m => m.GameId);

        builder.HasMany(m => m.MatchMembers)
            .WithOne(mm => mm.Match)
            .HasForeignKey(mm => mm.MatchId);
    }
}