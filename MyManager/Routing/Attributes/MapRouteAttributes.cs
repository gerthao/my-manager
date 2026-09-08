using System.Diagnostics.CodeAnalysis;

namespace MyManager.Routing.Attributes;


[AttributeUsage(AttributeTargets.Method)]
public abstract class MapRouteAttribute(string verb, [StringSyntax("Route")] string pattern): Attribute
{
    public string Verb { get; } = verb;
    public string Pattern { get; } = pattern;
}

[AttributeUsage(AttributeTargets.Method)]
public sealed class MapGetAttribute([StringSyntax("Route")] string pattern)
    : MapRouteAttribute("GET", pattern);

[AttributeUsage(AttributeTargets.Method)]
public sealed class MapDeleteAttribute([StringSyntax("Route")] string pattern)
    : MapRouteAttribute("DELETE", pattern);

[AttributeUsage(AttributeTargets.Method)]
public sealed class MapPostAttribute([StringSyntax("Route")] string pattern)
    : MapRouteAttribute("POST", pattern);

[AttributeUsage(AttributeTargets.Method)]
public sealed class MapPutAttribute([StringSyntax("Route")] string pattern)
    : MapRouteAttribute("PUT", pattern);

[AttributeUsage(AttributeTargets.Method)]
public sealed class MapPatchAttribute([StringSyntax("Route")] string pattern)
    : MapRouteAttribute("PATCH", pattern);

[AttributeUsage(AttributeTargets.Method)]
public sealed class WithDescriptionAttribute(string description) : Attribute
{
    public string Description { get; } = description;
}

[AttributeUsage(AttributeTargets.Method)]
public sealed class WithDisplayNameAttribute(string displayName) : Attribute
{
    public string DisplayName { get; } = displayName;
}

[AttributeUsage(AttributeTargets.Method)]
public sealed class WithTagsAttribute(params string[] tags) : Attribute
{
    public string[] Tags { get; } = tags;
}

[AttributeUsage(AttributeTargets.Method)]
public sealed class WithNameAttribute(string name) : Attribute
{
    public string Name { get; } = name;
}

[AttributeUsage(AttributeTargets.Method)]
public sealed class WithOrderAttribute(int order): Attribute
{
    public int Order { get; } = order;
}

[AttributeUsage(AttributeTargets.Method)]
public sealed class WithGroupNameAttribute(string groupName) : Attribute
{
    public string GroupName { get; } = groupName;
}

[AttributeUsage(AttributeTargets.Method)]
public sealed class WithSummaryAttribute(string summary) : Attribute
{
    public string Summary { get; } = summary;
}

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public sealed class AddEndpointFilterAttribute<T>() : Attribute where T: IEndpointFilter;

[AttributeUsage(AttributeTargets.Method)]
public sealed class AllowAnonymousAttribute(): Attribute;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public sealed class ProducesAttribute(int statusCode): Attribute
{
    public int StatusCode { get; } = statusCode;
}

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public sealed class ProducesAttribute<T>(int statusCode): Attribute
{
    public int StatusCode { get; } = statusCode;
}
