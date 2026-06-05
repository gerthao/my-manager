using System.ComponentModel.DataAnnotations;
using MyManager.Common.Domain.Entities;

namespace MyManager.Modules.ProjectBoard.Domain.Entities;

public sealed class Card : ICreateTimestampedEntity, IModifyTimestampedEntity
{
    public int Id { get; init; }

    [MaxLength(100)]
    public required string Name { get; set; }

    public required string? Description { get; set; }

    public int ProjectBucketId { get; set; }

    public int ProjectId { get; set; }

    public Column Column { get; set; } = null!;

    public Board Board { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset ModifiedAt { get; set; }
}
