using System.Runtime.CompilerServices;
using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Commands.Base;
using DCCRailway.Controller.Attributes;

[assembly: InternalsVisibleTo(" DCCRailway.Test.Manufacturers.NCE.VirtualPowerCabSensorTests")]

namespace DCCRailway.Controller.Virtual.Actions.Commands;

[Command("SensorGetState", "Get the state of a given Sensor")]
public class VirtualSensorGetState : VirtualCommand, ICmdSensorGetState, IAccyCmd {
    public override string      ToString() => $"GET SENSOR STATE ({Address.Address})";
    public          DCCAddress Address    { get; set; }
}