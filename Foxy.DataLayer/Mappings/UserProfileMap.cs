using Foxy.Core.Infrastructures.Constants;
using Foxy.DataLayer.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Foxy.DataLayer.Mappings;

public class UserProfileMap : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.HasKey(up => up.Id);

        builder.Property(up => up.AccountId)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(up => up.Bio)
            .HasMaxLength(200);

        builder.Property(up => up.ProfileImageGuid)
            .HasMaxLength(EntityConstants.ImageGuidSize);

        builder.Property(up => up.HeaderImageGuid)
            .HasMaxLength(EntityConstants.ImageGuidSize);

        builder.HasMany(up => up.MatchMembers)
            .WithOne(mm => mm.UserProfile)
            .HasForeignKey(mm => mm.UserProfileId);

        builder.HasMany(up => up.UserItems)
            .WithOne(ui => ui.foxyUser)
            .HasForeignKey(ui => ui.UserProfileId);

        builder.HasMany(up => up.SuggestionVotes)
            .WithOne(sv => sv.UserProfile)
            .HasForeignKey(sv => sv.UserProfileId);

        builder.HasMany(up => up.Tickets)
            .WithOne(t => t.UserProfile)
            .HasForeignKey(t => t.CreatedBy);
    }
}