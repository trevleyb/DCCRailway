using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DCCRailway.DCCController.Commands;
using DCCRailway.DCCController.Commands.Types;
using DCCRailway.DCCController.Commands.Types.BaseTypes;
using DCCRailway.DCCController.Types;
using DCCRailway.Manufacturer.Virtual.Commands.Results;
using DCCRailway.Manufacturer.Virtual.Commands.Validators;
using DCCRailway.Utilities;

[assembly: InternalsVisibleTo("DCCRailway.Delete.Test.VirtualPowerCabSensorTests")]

namespace DCCRailway.Manufacturer.Virtual.Commands;

[Command("SensorGetState", "Get the state of a given Sensor")]
public class VirtualSensorGetState : VirtualCommand, ICmdSensorGetState, IAccyCmd {
    public override string      ToString() => $"GET SENSOR STATE ({Address.Address})";
    public          IDCCAddress Address    { get; set; }
}