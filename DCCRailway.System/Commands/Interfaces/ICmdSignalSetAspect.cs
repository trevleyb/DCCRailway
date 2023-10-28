using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.Interfaces;

public interface ICmdSignalSetAspect : ICommand {
    public IDCCAddress Address { get; set; }
    public byte        Aspect  { get; set; }
    public bool        Off     { set; }
}