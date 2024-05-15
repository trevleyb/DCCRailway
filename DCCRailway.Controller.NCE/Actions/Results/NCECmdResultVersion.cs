using DCCRailway.Controller.Actions.Results.Abstract;

namespace DCCRailway.Controller.NCE.Actions.Results;

public class NCECmdResultVersion : CmdResult {
    public NCECmdResultVersion(byte[] dataSet) : base(dataSet) {
        if (Data.Length != 3) {
            Success = false;
        } else {
            Version = Data[0];
            Major   = Data[1];
            Minor   = Data[2];
        }
    }

    public string ToVersionString                => $"{Version}.{Major}.{Minor}";
    public bool   IsVersionMatch(string compare) => IsVersionMatch(ToVersionString, compare);

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