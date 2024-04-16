using System.Diagnostics;

namespace DCCRailway.System.Attributes;

[DebuggerDisplay("Name: {Name}, Description: {Description}, Version: {Version}")]
[AttributeUsage(AttributeTargets.Class)]
public class CommandAttribute : Attribute {

    public  string       Name        { get; set; }
    public  string       Description { get; set; }
    public  string       Version     { get; set; }
    private List<string> Supported   { get; }
    private List<string> UnSupported { get; }

    // The Supported and Not-Supported properties define the conditions underwhich this command is available.
    // The parameter must be adapter@version where version is optional.
    // If a command is ONLY supported by a particular adapter@version then it should be in the Supported list.
    // If a command is NOT supported by a particular adapter@version then it should be in the NotSupported list.
    // if the supported list is empty/null then ALL adapters and all versions are supported.
    // if the not-supported list if empty/null then no adapters or version are excluded.
    // if either is * then that means ALL (default for the supported list)

    public CommandAttribute(string name, string? description = null, string? version = null) {
        Name         = name;
        Version      = version ?? "";
        Description  = description ?? Name;
        Supported   = new List<string>() { "*@*" };
        UnSupported = [];
    }

    public CommandAttribute(string name, string description, string version, string[]? supported = null, string[]? unSupported = null) : this(name, description, version) {
        Supported = supported is null ? [] : supported.ToList();
        UnSupported = unSupported is null ? [] : unSupported.ToList();
    }

    public bool IsSupported(string adapterVersion) {
        var parts  = adapterVersion.Split('@', 2);
        var adapter = parts.Length > 0 ? parts[0] : "";
        var version = parts.Length > 1 ? parts[1] : "";
        return IsSupported(adapter, version);
    }

    public bool IsSupported(string adapter, string version) {
        if (string.IsNullOrWhiteSpace(adapter)) adapter = "*";
        if (string.IsNullOrWhiteSpace(version)) version = "*";
        var item = $"{adapter}@{version}";

        if (UnSupported.Contains(item) || UnSupported.Contains(adapter) || UnSupported.Contains($"{adapter}@*") || UnSupported.Contains($"@{version}") || UnSupported.Contains("*") || UnSupported.Contains("*@*")) {
            return false;
        }

        if (!Supported.Any() || Supported.Contains(item) || Supported.Contains(adapter) || Supported.Contains($"{adapter}@*") || Supported.Contains($"@{version}") || Supported.Contains("*") || Supported.Contains("*@*")) {
            return true;
        }
        return false;
    }
}