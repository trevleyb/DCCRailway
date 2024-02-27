using DCCRailway.Layout.Commands.Types.BaseTypes;

namespace DCCRailway.Layout.Commands.Types;

public interface ICmdLocoSetMomentum : ICommand, ILocoCmd {
    public byte        Momentum { get; set; }
}