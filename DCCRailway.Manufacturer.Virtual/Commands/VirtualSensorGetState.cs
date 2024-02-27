using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DCCRailway.Layout.Adapters;
using DCCRailway.Layout.Commands;
using DCCRailway.Layout.Commands.Results;
using DCCRailway.Layout.Commands.Types;
using DCCRailway.Layout.Commands.Types.BaseTypes;
using DCCRailway.Layout.Types;
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