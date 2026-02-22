using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Auctera.Auctions.Domain;

namespace Auctera.Persistance.Configurations.Auctions;

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

        builder.Property(a => a.LotId)
            .IsRequired();

        builder.HasIndex(a => a.LotId)
            .IsUnique();

        builder.OwnsOne(a => a.CurrentPrice, money =>
        {
            money.Property(m => m.Amount)
                .HasColumnName("current_price_amount")
                .HasColumnType("numeric(18,2)");

            money.Property(m => m.Currency)
                .HasColumnName("current_price_currency")
                .HasMaxLength(3);
        });

        builder.HasMany(a => a.Bids)
            .WithOne()
            .HasForeignKey("AuctionId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Auctera.Items.Domain.Lot>()
            .WithOne()
            .HasForeignKey<Auction>(a => a.LotId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
