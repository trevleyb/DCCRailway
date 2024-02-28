using DCCRailway.DCCController.Commands.Types.BaseTypes;

namespace DCCRailway.DCCController.Commands.Types;

/// <summary>
///     Turn the power to the controller ON or OFF
/// </summary>
public interface ICmdPowerGetState : ICommand, ISystemCmd { }