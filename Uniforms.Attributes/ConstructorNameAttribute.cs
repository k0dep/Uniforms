using System;

[AttributeUsage(AttributeTargets.Constructor)]
public sealed class ConstructorNameAttribute : Attribute
{
    public ConstructorNameAttribute(string name)
    {
        Name = name;
    }

    public readonly string Name;
}