namespace MyManager.Common.Domain.Entities;

public interface ICreateTimestampedEntity
{
    DateTimeOffset CreatedAt { get; set; }
}
