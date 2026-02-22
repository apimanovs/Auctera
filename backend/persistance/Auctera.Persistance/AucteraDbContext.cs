using Microsoft.EntityFrameworkCore;

using Auctera.Auctions.Domain;
using Auctera.Items.Domain;
using Auctera.Bids.Domain;
using Auctera.Identity.Domain;

namespace Auctera.Persistance;

public sealed class AucteraDbContext : DbContext
{
    public DbSet<Auction> Auctions => Set<Auction>();
    public DbSet<Lot> Lots => Set<Lot>();
    public DbSet<Bid> Bids => Set<Bid>();
    public DbSet<User> Users => Set<User>();

    public AucteraDbContext(DbContextOptions<AucteraDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AucteraDbContext).Assembly
        );
    }
}
