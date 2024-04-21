using DCCRailway.Common.Types;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands.Types;

namespace DCCRailway.Station.Virtual.Commands;

[Command("CVRead", "Read a CV from a Loco")]
public class VirtualCVRead : VirtualCommand, ICmdCVRead {
    public VirtualCVRead(int cv = 0) => CV = cv;

    public DCCProgrammingMode ProgrammingMode { get; set; }
    public int                CV              { get; set; }

    public override string       ToString() => $"READ CV ({CV}/{ProgrammingMode})";
    public          IDCCAddress? Address    { get; set; }
}