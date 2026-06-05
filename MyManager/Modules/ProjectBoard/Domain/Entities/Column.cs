using System.ComponentModel.DataAnnotations;
using MyManager.Common.Domain.Entities;

namespace MyManager.Modules.ProjectBoard.Domain.Entities;

public sealed class Column : ICreateTimestampedEntity, IModifyTimestampedEntity
{
    public int Id { get; init; }

    [MaxLength(100)]
    public required string Name { get; set; }

    [MaxLength(255)]
    public string? Description { get; set; }

    public int BoardId { get; set; }

    public ICollection<Card> Tasks { get; set; } = [];

    public Board Board { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset ModifiedAt { get; set; }
}
