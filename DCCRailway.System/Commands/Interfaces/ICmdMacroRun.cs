namespace DCCRailway.System.Commands.Interfaces;

public interface ICmdMacroRun : ICommand {
    public byte Macro { get; set; }
}