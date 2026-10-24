using InTown.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InTown.Data.Configuration;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Title).IsRequired().HasMaxLength(100);

        //One user creates many events
        builder.HasOne(e => e.CreatedBy)
            .WithMany(u => u.CreatedEvents)
            .HasForeignKey(e => e.CreatedByUserId);
    }

}