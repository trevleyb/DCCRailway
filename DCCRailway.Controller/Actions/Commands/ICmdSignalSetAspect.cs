using DCCRailway.Controller.Actions.Commands.Base;

namespace DCCRailway.Controller.Actions.Commands;

public interface ICmdSignalSetAspect : ICommand, ISignalCmd {
    public byte Aspect { get; set; }
    public bool Off    { set; }
}