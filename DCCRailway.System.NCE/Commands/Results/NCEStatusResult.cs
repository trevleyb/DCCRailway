using System;
using DCCRailway.System.Commands.Results;

namespace DCCRailway.System.NCE.Commands;

public class NCEStatusResult : ResultOK, IResultStatus {
    private readonly int _major;
    private readonly int _minor;

    private readonly int _version;

    public NCEStatusResult(byte[]? data) {
        if (data == null || data.Length != 3) throw new ApplicationException("Invalid data provided to create a Version");
        _version = data[0];
        _major = data[1];
        _minor = data[2];
    }

    public string Version => $"{_version}.{_major}.{_minor}";

    public override string ToString() {
        return Version;
    }
}