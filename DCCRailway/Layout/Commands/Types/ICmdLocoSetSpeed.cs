using DCCRailway.Layout.Commands.Types.BaseTypes;
using DCCRailway.Layout.Types;

namespace DCCRailway.Layout.Commands.Types;

public interface ICmdLocoSetSpeed : ICommand,ILocoCmd {
    public DCCProtocol  SpeedSteps { get; set; }
    public DCCDirection Direction  { get; set; }
    public byte         Speed      { get; set; }
}