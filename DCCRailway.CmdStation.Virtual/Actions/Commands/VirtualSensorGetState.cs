using System.Runtime.CompilerServices;
using DCCRailway.CmdStation.Actions.Commands;
using DCCRailway.CmdStation.Actions.Commands.Base;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.Common.Types;

[assembly: InternalsVisibleTo(" DCCRailway.Test.Manufacturers.NCE.VirtualPowerCabSensorTests")]

namespace DCCRailway.CmdStation.Virtual.Actions.Commands;

[Command("SensorGetState", "Get the state of a given Sensor")]
public class VirtualSensorGetState : VirtualCommand, ICmdSensorGetState, IAccyCmd {
    public override string      ToString() => $"GET SENSOR STATE ({Address.Address})";
    public          DCCAddress Address    { get; set; }
}