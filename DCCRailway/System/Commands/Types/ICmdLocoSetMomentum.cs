namespace DCCRailway.System.Commands.Types;

public interface ICmdLocoSetMomentum : ICommand,ILocoCommand {
    public byte        Momentum { get; set; }
}