using DCCRailway.CmdStation.Actions.Commands.Base;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Actions.Commands;

public interface ICmdLocoSetSpeed : ICommand, ILocoCmd {
    public DCCProtocol  SpeedSteps { get; set; }
    public DCCDirection Direction  { get; set; }
    public DCCSpeed     Speed      { get; set; }
}