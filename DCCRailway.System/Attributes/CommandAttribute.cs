namespace DCCRailway.System.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class CommandAttribute : Attribute {

    public CommandAttribute(string name, string? description = null, string? version = null) {
        Name              = name;
        Version           = version ?? "";
        Description       = description ?? Name;
        SupportedVersions = [];
        SupportedAdapters = [];
    }

    public CommandAttribute(string name, string description, string[] adapters, string[] versions) : this(name, description, "", adapters, versions) { }

    public CommandAttribute(string name, string description, string version, string[] adapters, string[] versions) : this(name, description, version) {
        SupportedAdapters = adapters.ToList();
        SupportedVersions = versions.ToList();
    }

    public string       Description { get; set; }
    public string       Version     { get; set; }
    public string       Name        { get; set; }
    public List<string> SupportedAdapters { get; }
    public List<string> SupportedVersions { get; }

    public bool ShouldInclude(string adapter, string version) {
        return ShouldIncludeCheck(SupportedAdapters,adapter) &&
               ShouldIncludeCheck(SupportedVersions, version);
    }

    private bool IsExcluded(List<string> list, string value) {
        return list.Contains("!*") || list.Any(item => item.StartsWith('!') && item.Remove(0, 1).Equals(value, StringComparison.InvariantCultureIgnoreCase));
    }

    private bool IsIncluded(List<string> list, string value) {
        return !list.Any() || list.Contains("*") || list.Any(item => item.Equals(value, StringComparison.InvariantCultureIgnoreCase));
    }

    private bool ShouldIncludeCheck(List<string> list, string value) {
        return !IsExcluded(list, value) && IsIncluded(list, value);
    }
}