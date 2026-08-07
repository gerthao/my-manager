namespace MyManager.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public abstract class MapRouteAttribute(string verb, string pattern, string? name = null, string? displayName = null, string[]? tags = null): Attribute
{
    public string Verb { get; } = verb;
    public string Pattern { get; } = pattern;
    public string? Name { get; } = name;
    public string? DisplayName { get; } = displayName;
    public string[]? Tags { get; } = tags;
}

[AttributeUsage(AttributeTargets.Method)]
public sealed class MapGetAttribute(string pattern, string? name = null, string? displayName = null, string[]? tags = null)
    : MapRouteAttribute("GET", pattern, name, displayName, tags);

[AttributeUsage(AttributeTargets.Method)]
public sealed class MapDeleteAttribute(string pattern, string? name = null, string? displayName = null, string[]? tags = null)
    : MapRouteAttribute("DELETE", pattern, name, displayName, tags);


[AttributeUsage(AttributeTargets.Method)]
public sealed class MapPostAttribute(string pattern, string? name = null, string? displayName = null, string[]? tags = null)
    : MapRouteAttribute("POST", pattern, name, displayName, tags);

[AttributeUsage(AttributeTargets.Method)]
public sealed class MapPutAttribute(string pattern, string? name = null, string? displayName = null, string[]? tags = null)
    : MapRouteAttribute("PUT", pattern, name, displayName, tags);

public sealed class WithDescriptionAttribute(string description) : Attribute
{
    public string Description { get; } = description;
}

public sealed class WithDisplayNameAttribute(string displayName) : Attribute
{
    public string DisplayName { get; } = displayName;
}

public sealed class WithTagsAttribute(string[] tags) : Attribute
{
    public string[] Tags { get; } = tags;
}

public sealed class WithNameAttribute(string name) : Attribute
{
    public string Name { get; } = name;
}

public sealed class WithOrderAttribute(int order): Attribute
{
    public int Order { get; } = order;
}

public sealed class WithGroupNameAttribute(string groupName) : Attribute
{
    public string GroupName { get; } = groupName;
}

public sealed class WithSummaryAttribute(string summary) : Attribute
{
    public string Summary { get; } = summary;
}

public sealed class WithMetadataAttribute(params object[] metadata) : Attribute
{
    public object[] Metadata { get; } = metadata;
}
