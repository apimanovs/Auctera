using Microsoft.EntityFrameworkCore;

using Auctera.Auctions.Domain;
using Auctera.Items.Domain;
using Auctera.Bids.Domain;
using Auctera.Identity.Domain;
using Auctera.Orders.Domain;

namespace Auctera.Persistance;

/// <summary>
/// Represents the auctera db context class.
/// </summary>
public sealed class AucteraDbContext : DbContext
{
    public DbSet<Auction> Auctions => Set<Auction>();
    public DbSet<Lot> Lots => Set<Lot>();
    public DbSet<Bid> Bids => Set<Bid>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Order> Orders => Set<Order>();

    public AucteraDbContext(DbContextOptions<AucteraDbContext> options) : base(options)
    {
    }
        
    /// <summary>
    /// Performs the on model creating operation.
    /// </summary>
    /// <param name="modelBuilder">Input data for the operation.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AucteraDbContext).Assembly
        );
    }
}
