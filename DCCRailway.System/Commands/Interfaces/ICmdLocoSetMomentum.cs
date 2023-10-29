using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.Interfaces;

public interface ICmdLocoSetMomentum : ICommand,ILocoCommand {
    public IDCCAddress Address  { get; set; }
    public byte        Momentum { get; set; }
}