using DCCRailway.System.Commands.Results;

namespace DCCRailway.System.Manufacturer.NCE.Commands.Results;

public class NCECommandResultVersion : CommandResult {
    public NCECommandResultVersion(CommandResultData? dataSet) : base(true, dataSet, null) {
        if (dataSet?.Data == null || dataSet.Length != 3) {
            IsSuccess = false;
        }
        else {
            Version = dataSet[0];
            Major   = dataSet[1];
            Minor   = dataSet[2];
        }
    }

    public string ToVersionString => $"{Version}.{Major}.{Minor}";

    public bool IsVersionMatch(string compare) => IsVersionMatch(ToVersionString, compare);

    public bool IsVersionMatch(string source, string compare) {
        var sourceSplit  = source.Split('.');
        var compareSplit = compare.Split('.');

        if (sourceSplit.Length != compareSplit.Length) return false;
        for (var i = 0; i < sourceSplit.Length; i++) {
            if (sourceSplit[i] != compareSplit[i] && compareSplit[i].ToUpperInvariant() != "X") return false;
        }

        return true;
    }

    public int Major   { get; }
    public int Minor   { get; }
    public int Version { get; }
}