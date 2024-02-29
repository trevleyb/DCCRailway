using DCCRailway.Common.Types;
using DCCRailway.System.Commands.Types.BaseTypes;

namespace DCCRailway.System.Commands.Types;

public interface ICmdLocoSetSpeed : ICommand, ILocoCmd {
    public DCCProtocol  SpeedSteps { get; set; }
    public DCCDirection Direction  { get; set; }
    public byte         Speed      { get; set; }
}