using System;

[AttributeUsage(AttributeTargets.Parameter)]
public sealed class IgnoreParameterAttribute : Attribute
{
    public readonly object DefaultValue;

    public IgnoreParameterAttribute(object defaultValue)
    {
        DefaultValue = defaultValue;
    }
}