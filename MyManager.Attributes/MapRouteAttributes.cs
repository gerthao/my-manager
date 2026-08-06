namespace MyManager.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public abstract class MapRouteAttribute(string verb, string pattern, string? name = null, string[]? tags = null): Attribute
{
    public string Verb { get; } = verb;
    public string Pattern { get; } = pattern;
    public string? Name { get; } = name;
    public string[]? Tags { get; } = tags;
}

[AttributeUsage(AttributeTargets.Method)]
public sealed class MapGetAttribute(string pattern, string? name = null, string[]? tags = null)
    : MapRouteAttribute("GET", pattern, name, tags);

[AttributeUsage(AttributeTargets.Method)]
public sealed class MapDeleteAttribute(string pattern, string? name = null, string[]? tags = null)
    : MapRouteAttribute("DELETE", pattern, name, tags);


[AttributeUsage(AttributeTargets.Method)]
public sealed class MapPostAttribute(string pattern, string? name = null, string[]? tags = null)
    : MapRouteAttribute("POST", pattern, name, tags);

[AttributeUsage(AttributeTargets.Method)]
public sealed class MapPutAttribute(string pattern, string? name = null, string[]? tags = null)
    : MapRouteAttribute("PUT", pattern, name, tags);
