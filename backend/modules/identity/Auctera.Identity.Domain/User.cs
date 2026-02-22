namespace Auctera.Identity.Domain;

public sealed class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string UserName { get; private set; }
    public bool IsAdmin { get; private set; }

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
}
