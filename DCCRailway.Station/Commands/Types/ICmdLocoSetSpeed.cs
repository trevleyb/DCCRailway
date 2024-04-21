using DCCRailway.Common.Types;
using DCCRailway.Station.Commands.Types.Base;

namespace DCCRailway.Station.Commands.Types;

public interface ICmdLocoSetSpeed : ICommand, ILocoCmd {
    public DCCProtocol  SpeedSteps { get; set; }
    public DCCDirection Direction  { get; set; }
    public DCCSpeed     Speed      { get; set; }
}