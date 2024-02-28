using DCCRailway.DCCController.Commands.Types.BaseTypes;
using DCCRailway.DCCController.Types;

namespace DCCRailway.DCCController.Commands.Types;

public interface ICmdLocoSetSpeed : ICommand,ILocoCmd {
    public DCCProtocol  SpeedSteps { get; set; }
    public DCCDirection Direction  { get; set; }
    public byte         Speed      { get; set; }
}