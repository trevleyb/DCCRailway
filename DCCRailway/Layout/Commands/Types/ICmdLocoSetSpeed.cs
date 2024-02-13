using DCCRailway.System.Layout.Types;

namespace DCCRailway.System.Layout.Commands.Types;

public interface ICmdLocoSetSpeed : ICommand,ILocoCommand {
    public DCCProtocol  SpeedSteps { get; set; }
    public DCCDirection Direction  { get; set; }
    public byte         Speed      { get; set; }
}