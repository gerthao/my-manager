namespace MyManager.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public partial class RegisterScopedAttribute(System.Type? type = null): Attribute;

[AttributeUsage(AttributeTargets.Class)]
public partial class RegisterSingletonAttribute(System.Type? type = null): Attribute;

[AttributeUsage(AttributeTargets.Class)]
public partial class RegisterTransientAttribute(System.Type? type = null): Attribute;
