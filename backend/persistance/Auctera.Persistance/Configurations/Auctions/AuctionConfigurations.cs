using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Auctera.Auctions.Domain;
using Auctera.Shared.Domain.ValueObjects;

public sealed class AuctionConfiguration : IEntityTypeConfiguration<Auction>
{
    public void Configure(EntityTypeBuilder<Auction> builder)
    {
        builder.ToTable("auctions");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(a => a.StartDate);
        builder.Property(a => a.EndDate);

        builder.OwnsOne(a => a.CurrentPrice, money =>
        {
            money.Property(m => m.Amount)
                .HasColumnName("current_price_amount")
                .HasColumnType("numeric(18,2)");

            money.Property(m => m.Currency)
                .HasColumnName("current_price_currency")
                .HasMaxLength(3);
        });

        // Auction -> Bids (1:N)
        builder.HasMany(a => a.Bids)
            .WithOne()
            .HasForeignKey("AuctionId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
