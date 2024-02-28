using DCCRailway.DCCController.Commands.Types.BaseTypes;

namespace DCCRailway.DCCController.Commands.Types;

public interface ICmdSignalSetAspect : ICommand,ISignalCmd {
    public byte        Aspect  { get; set; }
    public bool        Off     { set; }
}