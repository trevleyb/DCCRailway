using DCCRailway.CmdStation.Commands.Types.Base;

namespace DCCRailway.CmdStation.Commands.Types;

public interface ICmdSignalSetAspect : ICommand, ISignalCmd {
    public byte Aspect { get; set; }
    public bool Off    { set; }
}