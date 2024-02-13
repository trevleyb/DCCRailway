namespace DCCRailway.Layout.Commands.Types;

public interface ICmdLocoSetMomentum : ICommand,ILocoCommand {
    public byte        Momentum { get; set; }
}