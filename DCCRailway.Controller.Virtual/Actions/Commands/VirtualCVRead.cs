using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Attributes;

namespace DCCRailway.Controller.Virtual.Actions.Commands;

[Command("CVRead", "Read a CV from a Loco")]
public class VirtualCVRead : VirtualCommand, ICmdCVRead {
    public VirtualCVRead(int cv = 0) => CV = cv;

    public DCCProgrammingMode ProgrammingMode { get; set; }
    public int                CV              { get; set; }

    public override string       ToString() => $"READ CV ({CV}/{ProgrammingMode})";
    public          DCCAddress Address    { get; set; }
}