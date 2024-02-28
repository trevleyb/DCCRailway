using DCCRailway.DCCController.Commands.Types.BaseTypes;

namespace DCCRailway.DCCController.Commands.Types;

public interface ICmdLocoSetMomentum : ICommand, ILocoCmd {
    public byte        Momentum { get; set; }
}