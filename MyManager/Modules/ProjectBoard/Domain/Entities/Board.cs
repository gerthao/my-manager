using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MyManager.Common.Domain.Entities;

namespace MyManager.Modules.ProjectBoard.Domain.Entities;

[Index(nameof(Name), nameof(Slug), IsUnique = true)]
public sealed class Board : ICreateTimestampedEntity, IModifyTimestampedEntity
{
    public int Id { get; init; }

    [MinLength(1)]
    [MaxLength(100)]
    public required string Name { get; set; }

    [MaxLength(255)]
    public string? Description { get; set; }

    [MinLength(1)]
    [MaxLength(100)]
    public required string Slug { get; set; }

    public ICollection<Column> Columns { get; set; } = [];

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset ModifiedAt { get; set; }
}
