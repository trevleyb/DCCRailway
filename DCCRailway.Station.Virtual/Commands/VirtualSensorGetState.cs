using System.Runtime.CompilerServices;
using DCCRailway.Common.Types;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands.Types;
using DCCRailway.Station.Commands.Types.Base;

[assembly: InternalsVisibleTo(" DCCRailway.Test.Manufacturers.NCE.VirtualPowerCabSensorTests")]

namespace DCCRailway.Station.Virtual.Commands;

[Command("SensorGetState", "Get the state of a given Sensor")]
public class VirtualSensorGetState : VirtualCommand, ICmdSensorGetState, IAccyCmd {
    public override string      ToString() => $"GET SENSOR STATE ({Address.Address})";
    public          IDCCAddress Address    { get; set; }
}