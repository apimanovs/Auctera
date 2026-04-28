namespace Auctera.Identity.Domain;

/// <summary>
/// Represents the user class.
/// </summary>
public sealed class User
{
    /// <summary>
    /// Gets or sets the id used by this type.
    /// </summary>
    public Guid Id { get; private set; }
    /// <summary>
    /// Gets or sets the name used by this type.
    /// </summary>
    public string Name { get; private set; }
    /// <summary>
    /// Gets or sets the email used by this type.
    /// </summary>
    public string Email { get; private set; }
    /// <summary>
    /// Gets or sets the password hash used by this type.
    /// </summary>
    public string PasswordHash { get; private set; }
    /// <summary>
    /// Gets or sets the user name used by this type.
    /// </summary>
    public string UserName { get; private set; }
    public string? City { get; private set; }
    public string? Country { get; private set; }
    /// <summary>
    /// Gets or sets the is admin used by this type.
    /// </summary>
    public bool IsAdmin { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class.
    /// </summary>
    private User() { } // EF

    private User(Guid id,
        string name,
        string email,
        string passwordHash,
        string userName,
        bool isAdmin)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be empty");
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email cannot be empty");
        }

        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            throw new ArgumentException("Password hash cannot be empty");
        }

        if (string.IsNullOrWhiteSpace(userName))
        {
            throw new ArgumentException("Username cannot be empty");
        }

        Id = id;
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        UserName = userName;
        IsAdmin = isAdmin;
    }

    public static User Create(
        string name,
        string email,
        string passwordHash,
        string userName)
    {
        return new User(
            Guid.NewGuid(),
            name,
            email,
            passwordHash,
            userName,
            false);
    }

    public void UpdateProfile(string name, string userName, string? city, string? country)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be empty");
        }

        if (string.IsNullOrWhiteSpace(userName))
        {
            throw new ArgumentException("Username cannot be empty");
        }

        Name = name.Trim();
        UserName = userName.Trim();
        City = NormalizeOptional(city, 100, "City");
        Country = NormalizeOptional(country, 100, "Country");
    }

    private static string? NormalizeOptional(string? value, int maxLength, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        var normalized = value.Trim();

        if (normalized.Length > maxLength)
        {
            throw new ArgumentException($"{fieldName} must be {maxLength} characters or fewer.");
        }

        return normalized;
    }
}
