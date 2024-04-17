﻿using DCCRailway.System.Commands.Types.Base;

namespace DCCRailway.System.Commands.Types;

/// <summary>
///     Turn the power to the controller ON or OFF
/// </summary>
public interface ICmdPowerSetOff : ICommand, ISystemCmd { }