using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.Interfaces;

public interface ICmdLocoStop : ICommand,ILocoCommand {
    public IDCCAddress Address { get; set; }
}