using Foxy.Core.Infrastructures.Constants;
using Foxy.DataLayer.Models.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Foxy.DataLayer.Mappings;

public class TicketMap : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(EntityConstants.GeneralTitleSize);

        builder.Property(t => t.Description)
            .HasMaxLength(1000);

        builder.HasOne(t => t.UserProfile)
            .WithMany(up => up.Tickets)
            .HasForeignKey(n => n.CreatedBy);
    }
}