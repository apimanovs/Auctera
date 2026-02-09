using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auctera.Shared.Domain.Abstractions;

public class Entity<TId>
{
    public TId Id { get; protected set; } = default!;

    protected Entity() { }
    public Entity(TId id)
    {
        if (id == null || id.Equals(default))
        {
            throw new ArgumentException("Entity ID cannot be an empty GUID.", nameof(id));
        }

        Id = id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity<TId> other)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Id.Equals(other.Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
