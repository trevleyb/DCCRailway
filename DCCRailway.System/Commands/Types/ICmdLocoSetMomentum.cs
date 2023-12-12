using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.CommandType;

public interface ICmdLocoSetMomentum : ICommand,ILocoCommand {
    public IDCCAddress Address  { get; set; }
    public byte        Momentum { get; set; }
}