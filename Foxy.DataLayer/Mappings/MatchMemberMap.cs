using Foxy.DataLayer.Models.Games;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Foxy.DataLayer.Mappings;

public class MatchMemberMap : IEntityTypeConfiguration<MatchMember>
{
    public void Configure(EntityTypeBuilder<MatchMember> builder)
    {
        builder.HasKey(mm => mm.Id);

        builder.HasOne(mm => mm.Match)
            .WithMany(m => m.MatchMembers)
            .HasForeignKey(mm => mm.MatchId);

        builder.HasOne(mm => mm.UserProfile)
            .WithMany(up => up.MatchMembers)
            .HasForeignKey(mm => mm.UserProfileId);

        builder.Property(mm => mm.TeamName).HasMaxLength(16);
    }
}