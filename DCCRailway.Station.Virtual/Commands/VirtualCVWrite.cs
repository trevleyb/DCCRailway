using DCCRailway.Common.Types;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands.Types;

namespace DCCRailway.Station.Virtual.Commands;

[Command("CVWrite", "Write a value to a CV on a Loco")]
public class VirtualCVWrite : VirtualCommand, ICmdCVWrite {
    public VirtualCVWrite(int cv = 0, byte value = 0) {
        CV    = cv;
        Value = value;
    }

    public IDCCAddress?       Address         { get; set; }
    public DCCProgrammingMode ProgrammingMode { get; set; }
    public int                CV              { get; set; }
    public byte               Value           { get; set; }

    public override string ToString() => $"WRITE CV ({CV}={Value}/{ProgrammingMode})";
}