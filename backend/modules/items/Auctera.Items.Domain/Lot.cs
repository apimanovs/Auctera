using Auctera.Items.Domain.Enums;
using Auctera.Shared.Domain.Abstractions;
using Auctera.Shared.Domain.Enums;
using Auctera.Shared.Domain.ValueObjects;

namespace Auctera.Items.Domain;

/// <summary>
/// Represents the lot class.
/// </summary>
public sealed class Lot : Entity<Guid>
{
    /// <summary>
    /// Gets or sets the seller id used by this type.
    /// </summary>
    public Guid SellerId { get; private set; }

    /// <summary>
    /// Gets or sets the title used by this type.
    /// </summary>
    public string Title { get; private set; }
    /// <summary>
    /// Gets or sets the description used by this type.
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Gets or sets the price used by this type.
    /// </summary>
    public Money Price { get; private set; }
    /// <summary>
    /// Gets or sets the status used by this type.
    /// </summary>
    public LotStatus Status { get; private set; }

    /// <summary>
    /// Gets or sets the category used by this type.
    /// </summary>
    public LotCategory Category { get; private set; }
    /// <summary>
    /// Gets or sets the gender used by this type.
    /// </summary>
    public LotGender Gender { get; private set; }
    /// <summary>
    /// Gets or sets the size used by this type.
    /// </summary>
    public LotSize Size { get; private set; }
    /// <summary>
    /// Gets or sets the brand used by this type.
    /// </summary>
    public string Brand { get; private set; }
    /// <summary>
    /// Gets or sets the condition used by this type.
    /// </summary>
    public LotCondition Condition { get; private set; }
    /// <summary>
    /// Gets or sets the color used by this type.
    /// </summary>
    public string? Color { get; private set; }

    /// <summary>
    /// Gets or sets the media used by this type.
    /// </summary>
    public List<LotMedia> Media { get; private set; } = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="Lot"/> class.
    /// </summary>
    private Lot() { }

    public Lot(
        Guid id,
        Guid sellerId,
        string title,
        string description,
        Money price,
        LotCategory category,
        LotGender gender,
        LotSize size,
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

        if (size == null)
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
        Size = size;
        Brand = brand.Trim();
        Condition = condition;
        Color = string.IsNullOrWhiteSpace(color) ? null : color.Trim();
        Status = LotStatus.Draft;
    }

    public void Edit(
        Guid sellerId,
        string title,
        string description,
        Money price,
        LotCategory category,
        LotGender gender,
        LotSize size,
        string brand,
        LotCondition condition,
        string? color)
    {
        if (Status == LotStatus.Draft)
        {
            if (sellerId != SellerId)
            {
                throw new InvalidOperationException("Only the seller can edit this lot.");
            }

            if (string.IsNullOrWhiteSpace(title) || title.Trim().Length < 5 || title.Trim().Length > 200)
            {
                throw new ArgumentException("Title must be between 5 and 200 characters.");
            }

            if (string.IsNullOrWhiteSpace(description) || description.Trim().Length < 20 || description.Trim().Length > 2000)
            {
                throw new ArgumentException("Description must be between 20 and 2000 characters.");
            }

            if (price is null)
            {
                throw new ArgumentException("Price is required.");
            }

            if (!Enum.IsDefined(typeof(LotSize), size))
            {
                throw new ArgumentException("Size is required.");
            }

            if (string.IsNullOrWhiteSpace(brand))
            {
                throw new ArgumentException("Brand is required.");
            }

            if (!Enum.IsDefined(typeof(LotCondition), condition))
            {
                throw new ArgumentException("Condition is required.");
            }

            if (string.IsNullOrWhiteSpace(color))
            {
                throw new ArgumentException("Color is required.");
            }

        }
        else
        {
            throw new InvalidOperationException("Only draft lots can be edited.");
        }

        Title = title;
        Description = description;
        Price = price;
        Category = category;
        Gender = gender;
        Size = size;
        Brand = brand.Trim();
        Condition = condition;
        Color = color.Trim();
    }

    /// <summary>
    /// Publishes the operation.
    /// </summary>
    public void Publish()
    {
        if (Status != LotStatus.Draft)
        {
            throw new InvalidOperationException("Only draft lots can be published.");
        }

        Status = LotStatus.Published;
    }

    /// <summary>
    /// Adds photo.
    /// </summary>
    /// <param name="key">Key.</param>
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

    /// <summary>
    /// Represents the lot media class.
    /// </summary>
    public sealed class LotMedia
    {
        /// <summary>
        /// Gets or sets the key used by this type.
        /// </summary>
        public string Key { get; private set; }
        /// <summary>
        /// Gets or sets the type used by this type.
        /// </summary>
        public string Type { get; private set; } // photo, video, etc.

        /// <summary>
        /// Initializes a new instance of the <see cref="LotMedia"/> class.
        /// </summary>
        private LotMedia() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LotMedia"/> class.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="type">Type.</param>
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
