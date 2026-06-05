using System.Text.RegularExpressions;

namespace MyManager.Common;

public static partial class Slug
{
    [GeneratedRegex(@"[^a-z0-9\s-]")]
    private static partial Regex InvalidCharsRegex();

    [GeneratedRegex(@"\s+")]
    private static partial Regex WhitespaceRegex();

    [GeneratedRegex(@"-+")]
    private static partial Regex MultipleDashesRegex();

    public static string Slugify(string input)
    {
        var str = input.ToLowerInvariant();

        str = InvalidCharsRegex().Replace(str, "");
        str = WhitespaceRegex().Replace(str, " ").Trim();
        str = str.Replace(" ", "-");
        str = MultipleDashesRegex().Replace(str, "-");

        return str;
    }
}
