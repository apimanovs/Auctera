using Auctera.Items.Domain.Enums;
using Auctera.Shared.Domain.Abstractions;
using Auctera.Shared.Domain.Enums;
using Auctera.Shared.Domain.ValueObjects;

namespace Auctera.Items.Domain;

public sealed class Lot : Entity<Guid>
{
    public Guid SellerId { get; private set; }

    public string Title { get; private set; }
    public string Description { get; private set; }

    public Money Price { get; private set; }
    public LotStatus Status { get; private set; }

    public LotCategory Category { get; private set; }
    public LotGender Gender { get; private set; }
    public string Size { get; private set; }
    public string Brand { get; private set; }
    public LotCondition Condition { get; private set; }
    public string? Color { get; private set; }

    public List<LotMedia> Media { get; private set; } = new();

    private Lot() { }

    public Lot(
        Guid id,
        Guid sellerId,
        string title,
        string description,
        Money price,
        LotCategory category,
        LotGender gender,
        string size,
        string brand,
        LotCondition condition,
        string? color
    ) : base(id)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title is required.");
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Description is required.");
        }

        if (description.Length < 100)
        {
            throw new ArgumentException("Description must be at least 100 characters.");
        }

        if (string.IsNullOrWhiteSpace(size))
        {
            throw new ArgumentException("Size is required.");
        }

        if (string.IsNullOrWhiteSpace(brand))
        {
            throw new ArgumentException("Brand is required.");
        }

        SellerId = sellerId;
        Title = title;
        Description = description;
        Price = price ?? throw new ArgumentNullException(nameof(price));
        Category = category;
        Gender = gender;
        Size = size.Trim();
        Brand = brand.Trim();
        Condition = condition;
        Color = string.IsNullOrWhiteSpace(color) ? null : color.Trim();
        Status = LotStatus.Draft;
    }

    public void Publish()
    {
        if (Status != LotStatus.Draft)
        {
            throw new InvalidOperationException("Only draft lots can be published.");
        }

        Status = LotStatus.Published;
    }

    public void AddPhoto(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException("Media key is required.");
        }

        var normalizedKey = key.Trim();

        if (Media.Any(m => m.Key == normalizedKey && m.Type == "photo"))
        {
            return;
        }

        Media.Add(new LotMedia(normalizedKey, "photo"));
    }

    public sealed class LotMedia
    {
        public string Key { get; private set; }
        public string Type { get; private set; } // photo, video, etc.

        private LotMedia() { }

        public LotMedia(string key, string type)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("Media key is required.");
            }

            if (string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentException("Media type is required.");
            }

            Key = key;
            Type = type;
        }
    }
}
