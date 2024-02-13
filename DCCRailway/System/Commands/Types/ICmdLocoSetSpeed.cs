using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.Types;

public interface ICmdLocoSetSpeed : ICommand,ILocoCommand {
    public DCCProtocol  SpeedSteps { get; set; }
    public DCCDirection Direction  { get; set; }
    public byte         Speed      { get; set; }
}