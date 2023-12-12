namespace DCCRailway.System.Commands.CommandType;

public interface ICmdMacroRun : ICommand {
    public byte Macro { get; set; }
}