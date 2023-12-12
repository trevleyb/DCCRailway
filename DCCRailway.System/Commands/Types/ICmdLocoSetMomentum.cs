using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.CommandType;

public interface ICmdLocoSetMomentum : ICommand,ILocoCommand {
    public byte        Momentum { get; set; }
}