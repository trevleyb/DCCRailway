using DCCRailway.Layout.Commands.Types.BaseTypes;

namespace DCCRailway.Layout.Commands.Types;

/// <summary>
///     Turn the power to the controller ON or OFF
/// </summary>
public interface ICmdPowerSetOff : ICommand, ISystemCmd { }