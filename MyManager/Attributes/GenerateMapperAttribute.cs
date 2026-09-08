namespace MyManager.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class GenerateMapperAttribute(Type target) : Attribute
{
    public Type Target { get; } = target;
}
