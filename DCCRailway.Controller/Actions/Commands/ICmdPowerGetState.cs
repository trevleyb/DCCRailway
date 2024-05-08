using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions.Commands.Base;

namespace DCCRailway.Controller.Actions.Commands;

/// <summary>
///     Turn the power to the controller ON or OFF
/// </summary>
public interface ICmdPowerGetState : ICommand, ISystemCmd {
    DCCPowerState State { get; set; }
}