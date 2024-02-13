namespace DCCRailway.System.Commands.Types;

public interface ICmdMacroRun : ICommand {
    public byte Macro { get; set; }
}