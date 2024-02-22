using DCCRailway.Layout.Adapters;
using DCCRailway.Layout.Commands;
using DCCRailway.Layout.Commands.Results;
using DCCRailway.Layout.Commands.Types;
using DCCRailway.Layout.Types;
using DCCRailway.Manufacturer.Virtual.Commands.Validators;
using DCCRailway.Utilities;
using DCCRailway.Utilities.Exceptions;

namespace DCCRailway.Manufacturer.Virtual.Commands;

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