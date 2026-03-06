using Auctera.Orders.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auctera.Persistance.Configurations.Orders;

public sealed class OrdersConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");

        builder.HasIndex(user => user.Id).IsUnique();

        builder.HasKey(x => x.Id);

        builder.Property(x => x.AuctionId).IsRequired();
        builder.Property(x => x.SellerId).IsRequired();
        builder.Property(x => x.BuyerId).IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(x => x.Price)
            .HasColumnType("numeric(18,2)")
            .IsRequired();

        builder.Property(x => x.Currency)
            .HasMaxLength(3)
            .IsRequired();

        builder.Property(x => x.PaymentDeadlineUtc).IsRequired();
        builder.Property(x => x.StripeCheckoutSessionId).HasMaxLength(200);
        builder.Property(x => x.PaidAtUtc);

        builder.HasIndex(x => x.AuctionId).IsUnique();
        builder.HasIndex(x => new { x.Status, x.PaymentDeadlineUtc });
        builder.HasIndex(x => x.BuyerId);
        builder.HasIndex(x => x.SellerId);
    }
}
