using Auctera.Items.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auctera.Persistance.Configurations.Items;

public sealed class LotConfiguration : IEntityTypeConfiguration<Lot>
{
    public void Configure(EntityTypeBuilder<Lot> builder)
    {
        builder.ToTable("lots");

        builder.HasKey(lot => lot.Id);

        builder.Property(l => l.SellerId)
            .IsRequired();

        builder.Property(lot => lot.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(lot => lot.Description)
            .IsRequired()
            .HasMaxLength(5000);

        builder.Property(lot => lot.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.OwnsOne(lot => lot.Price, money =>
        {
            money.Property(m => m.Amount)
                .IsRequired()
                .HasColumnName("price_amount")
                .HasColumnType("numeric(18,2)");

            money.Property(m => m.Currency)
                .IsRequired()
                .HasColumnName("price_currency")
                .HasMaxLength(3);
        });

        builder.OwnsMany(l => l.Media, media =>
        {
            media.ToTable("lot_media");

            media.WithOwner().HasForeignKey("LotId");

            media.Property<int>("Id");
            media.HasKey("Id");

            media.Property(m => m.Key)
                .HasColumnName("media_key")
                .HasMaxLength(500)
                .IsRequired();

            media.Property(m => m.Type)
                .HasColumnName("media_type")
                .HasMaxLength(50)
                .IsRequired();

            media.HasIndex("LotId", "media_key", "media_type")
                .IsUnique();
        });
    }
}
