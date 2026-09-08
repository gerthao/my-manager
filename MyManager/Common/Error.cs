namespace MyManager.Common;

public record Error(string Code, string Description)
{
    public static readonly Error Empty = new(string.Empty, string.Empty);
}
