using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Commands.Types;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Virtual.Commands;

[Command("CVRead", "Read a CV from a Loco")]
public class VirtualCVRead : VirtualCommand, ICmdCVRead {
    public VirtualCVRead(int cv = 0) => CV = cv;

    public DCCProgrammingMode ProgrammingMode { get; set; }
    public int                CV              { get; set; }

    public override string       ToString() => $"READ CV ({CV}/{ProgrammingMode})";
    public          DCCAddress? Address    { get; set; }
}