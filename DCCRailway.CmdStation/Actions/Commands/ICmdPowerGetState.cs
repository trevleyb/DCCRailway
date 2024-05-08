using DCCRailway.CmdStation.Actions.Commands.Base;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Actions.Commands;

/// <summary>
///     Turn the power to the controller ON or OFF
/// </summary>
public interface ICmdPowerGetState : ICommand, ISystemCmd {
    DCCPowerState State { get; set; }
}