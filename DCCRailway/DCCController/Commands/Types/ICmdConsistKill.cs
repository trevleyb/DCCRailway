using DCCRailway.DCCController.Commands.Types.BaseTypes;
using DCCRailway.DCCController.Types;

namespace DCCRailway.DCCController.Commands.Types;

public interface ICmdConsistKill : ICommand,IConsistCmd {
    public IDCCAddress Address { get; set; }
}