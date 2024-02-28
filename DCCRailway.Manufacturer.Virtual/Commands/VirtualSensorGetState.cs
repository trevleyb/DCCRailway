using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DCCRailway.Common.Types;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands.Types;
using DCCRailway.System.Commands.Types.BaseTypes;
using DCCRailway.Manufacturer.Virtual.Commands.Results;
using DCCRailway.Manufacturer.Virtual.Commands.Validators;

[assembly: InternalsVisibleTo("DCCRailway.Delete.Test.VirtualPowerCabSensorTests")]

namespace DCCRailway.Manufacturer.Virtual.Commands;

[Command("SensorGetState", "Get the state of a given Sensor")]
public class VirtualSensorGetState : VirtualCommand, ICmdSensorGetState, IAccyCmd {
    public override string      ToString() => $"GET SENSOR STATE ({Address.Address})";
    public          IDCCAddress Address    { get; set; }
}