using Foxy.Core.Infrastructures.Constants;
using Foxy.DataLayer.Models.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Foxy.DataLayer.Mappings;

public class SuggestionMap : IEntityTypeConfiguration<Suggestion>
{
    public void Configure(EntityTypeBuilder<Suggestion> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Title)
            .IsRequired()
            .HasMaxLength(EntityConstants.GeneralTitleSize);
        
        builder.Property(s => s.Description)
            .HasMaxLength(1000);
        
        builder.Property(s => s.ConfirmedDescription)
            .HasMaxLength(1000);

        builder.HasMany(s => s.SuggestionVotes)
            .WithOne(sv => sv.Suggestion)
            .HasForeignKey(sv => sv.SuggestionId);
    }
}