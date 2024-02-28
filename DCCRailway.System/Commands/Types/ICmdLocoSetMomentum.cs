using DCCRailway.System.Commands.Types.BaseTypes;

namespace DCCRailway.System.Commands.Types;

public interface ICmdLocoSetMomentum : ICommand, ILocoCmd {
    public byte        Momentum { get; set; }
}