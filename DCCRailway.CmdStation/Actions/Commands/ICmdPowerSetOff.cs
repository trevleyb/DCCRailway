using DCCRailway.CmdStation.Actions.Commands.Base;

namespace DCCRailway.CmdStation.Actions.Commands;

/// <summary>
///     Turn the power to the controller ON or OFF
/// </summary>
public interface ICmdPowerSetOff : ICommand, ISystemCmd { }