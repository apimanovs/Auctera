using Auctera.Bids.Domain;

using Microsoft.EntityFrameworkCore;

namespace Auctera.Persistance.Configurations.Bids;

public sealed class BidsConfiguration : IEntityTypeConfiguration<Bid>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Bid> builder)
    {
        builder.ToTable("bids");

        builder.HasKey(bid => bid.Id);

        builder.Property(bid => bid.BidderId)
            .IsRequired();

        builder.Property(bid => bid.PlacedAt)
            .IsRequired();

        builder.Property(bid => bid.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.OwnsOne(bid => bid.Amount, money =>
        {
            money.Property(m => m.Amount)
                .IsRequired()
                .HasColumnName("amount_value")
                .HasColumnType("numeric(18,2)");

            money.Property(m => m.Currency)
                .IsRequired()
                .HasColumnName("amount_currency")
                .HasMaxLength(3);
        });
    }
}
