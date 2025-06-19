using Foxy.DataLayer.Models.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Foxy.DataLayer.Mappings;

public class SuggestionVoteMap : IEntityTypeConfiguration<SuggestionVote>
{
    public void Configure(EntityTypeBuilder<SuggestionVote> builder)
    {
        builder.HasKey(sv => sv.Id);

        builder.HasOne(sv => sv.Suggestion)
            .WithMany(s => s.SuggestionVotes)
            .HasForeignKey(sv => sv.SuggestionId);

        builder.HasOne(sv => sv.UserProfile)
            .WithMany(up => up.SuggestionVotes)
            .HasForeignKey(sv => sv.UserProfileId);
    }
}