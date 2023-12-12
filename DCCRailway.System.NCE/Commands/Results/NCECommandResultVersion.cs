using System;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.Types;

namespace DCCRailway.System.NCE.Commands;

public class NCECommandResultVersion : CommandResultMessage {

    public NCECommandResultVersion(CommandResultDataSet dataSet) : base(null, "") {
        if (dataSet.Data == null || dataSet.Bytes != 3) throw new ApplicationException("Invalid data provided to create a Version");
            Version = dataSet[0];
            Major   = dataSet[1];
            Minor   = dataSet[2];
    }
    public new string ToString => $"{Version}.{Major}.{Minor}";

    public int Major    { get; }
    public int Minor    { get; }
    public int Version { get; }
}