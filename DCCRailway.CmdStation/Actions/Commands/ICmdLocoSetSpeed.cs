using DCCRailway.CmdStation.Commands.Types.Base;
using DCCRailway.Common.Types;
using DCCRailway.CmdStation.Attributes;

namespace DCCRailway.CmdStation.Commands.Types;

public interface ICmdLocoSetSpeed : ICommand, ILocoCmd {
    public DCCProtocol  SpeedSteps { get; set; }
    public DCCDirection Direction  { get; set; }
    public DCCSpeed     Speed      { get; set; }
}