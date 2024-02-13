namespace DCCRailway.System.Layout.Commands.Types;

public interface ICmdLocoSetMomentum : ICommand,ILocoCommand {
    public byte        Momentum { get; set; }
}