using DCCRailway.CmdStation.Actions.Commands.Base;

namespace DCCRailway.CmdStation.Actions.Commands;

public interface ICmdSignalSetAspect : ICommand, ISignalCmd {
    public byte Aspect { get; set; }
    public bool Off    { set; }
}