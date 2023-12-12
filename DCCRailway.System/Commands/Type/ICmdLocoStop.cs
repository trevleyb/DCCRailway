using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.CommandType;

public interface ICmdLocoStop : ICommand,ILocoCommand {
    public IDCCAddress Address { get; set; }
}