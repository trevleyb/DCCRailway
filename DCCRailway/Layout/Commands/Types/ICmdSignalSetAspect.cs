using DCCRailway.Layout.Commands.Types.BaseTypes;

namespace DCCRailway.Layout.Commands.Types;

public interface ICmdSignalSetAspect : ICommand,ISignalCmd {
    public byte        Aspect  { get; set; }
    public bool        Off     { set; }
}