namespace MyManager.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class RegisterScopedAttribute(Type? type = null): Attribute
{
    public Type? Type { get; } = type;
}

[AttributeUsage(AttributeTargets.Class)]
public class RegisterSingletonAttribute(Type? type = null): Attribute
{
    public Type? Type { get; } = type;
}

[AttributeUsage(AttributeTargets.Class)]
public class RegisterTransientAttribute(Type? type = null): Attribute
{
    public Type? Type { get; } = type;
}
