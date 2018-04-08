using System;

[AttributeUsage(AttributeTargets.Parameter)]
public sealed class ParameterAttribute : Attribute
{
    public readonly string Name;
    public readonly object DefaultValue;

    public ParameterAttribute(string name, object defaultValue)
    {
        Name = name;
        DefaultValue = defaultValue;
    }
}