namespace DCCRailway.Layout.Commands.Types;

public interface ICmdMacroRun : ICommand {
    public byte Macro { get; set; }
}