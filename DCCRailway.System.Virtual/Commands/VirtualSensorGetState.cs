﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DCCRailway.Common.Types;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands.Types;
using DCCRailway.System.Commands.Types.Base;
using DCCRailway.System.Virtual.Commands.Results;
using DCCRailway.System.Virtual.Commands.Validators;

[assembly: InternalsVisibleTo(" DCCRailway.Test.Manufacturers.NCE.VirtualPowerCabSensorTests")]

namespace DCCRailway.System.Virtual.Commands;

[Command("SensorGetState", "Get the state of a given Sensor")]
public class VirtualSensorGetState : VirtualCommand, ICmdSensorGetState, IAccyCmd {
    public override string      ToString() => $"GET SENSOR STATE ({Address.Address})";
    public          IDCCAddress Address    { get; set; }
}