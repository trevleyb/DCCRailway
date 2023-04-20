namespace DCCRailway.Core.Systems.Commands.Interfaces; 

public interface ICmdMacroRun : ICommand {
    public byte Macro { get; set; }
}