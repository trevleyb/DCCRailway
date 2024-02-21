namespace DCCRailway.Layout.Commands.Types;

public interface ICmdLocoSetMomentum : ICommand,ICmdWithAddress {
    public byte        Momentum { get; set; }
}