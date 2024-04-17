using DCCRailway.Common.Types;
using DCCRailway.System.Commands.Types.Base;

namespace DCCRailway.System.Commands.Types;

public interface ICmdLocoSetSpeed : ICommand, ILocoCmd {
    public DCCProtocol  SpeedSteps { get; set; }
    public DCCDirection Direction  { get; set; }
    public DCCSpeed     Speed      { get; set; }
}