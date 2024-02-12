using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.CommandType;

public interface ICmdSignalSetAspect : ICommand,IAccyCommand {
    public byte        Aspect  { get; set; }
    public bool        Off     { set; }
}