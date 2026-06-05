using Microsoft.EntityFrameworkCore;
using MyManager.Common.Domain.Entities;
using MyManager.Modules.ProjectBoard.Domain.Entities;

namespace MyManager.Modules.ProjectBoard;

public sealed class ProjectBoardDbContext(DbContextOptions<ProjectBoardDbContext> options) : DbContext(options)
{
    public DbSet<Board> Boards { get; set; }
    public DbSet<Column> Columns { get; set; }
    public DbSet<Card> Cards { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTimeOffset.UtcNow;

        foreach (var entry in ChangeTracker.Entries<ICreateTimestampedEntity>())
            if (entry.State == EntityState.Added)
                entry.Entity.CreatedAt = now;

        foreach (var entry in ChangeTracker.Entries<IModifyTimestampedEntity>())
            if (entry.State is EntityState.Modified or EntityState.Added)
                entry.Entity.ModifiedAt = now;


        return await base.SaveChangesAsync(cancellationToken);
    }
}
