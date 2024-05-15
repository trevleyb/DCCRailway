namespace DCCRailway.Common.Parameters;

[AttributeUsage(AttributeTargets.Property)]
public class ParameterAttribute(string description, string options, object? defaultValue = null)
    : Attribute {
    public readonly object? Default = defaultValue;

    public readonly string? Description = description;
    public readonly string? Options     = options;
    public ParameterAttribute(string description) : this(description, "") { }
    public ParameterAttribute(string description, object defaultValue) : this(description, "", defaultValue) { }
}