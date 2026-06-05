namespace MyManager.Common.Domain.Entities;

public interface IModifyTimestampedEntity
{
    DateTimeOffset ModifiedAt { get; set; }
}
