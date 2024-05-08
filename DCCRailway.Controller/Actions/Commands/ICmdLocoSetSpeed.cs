using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions.Commands.Base;

namespace DCCRailway.Controller.Actions.Commands;

public interface ICmdLocoSetSpeed : ICommand, ILocoCmd {
    public DCCProtocol  SpeedSteps { get; set; }
    public DCCDirection Direction  { get; set; }
    public DCCSpeed     Speed      { get; set; }
}