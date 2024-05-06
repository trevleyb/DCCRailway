using System.Runtime.CompilerServices;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Commands.Types;
using DCCRailway.CmdStation.Commands.Types.Base;
using DCCRailway.Common.Types;

[assembly: InternalsVisibleTo(" DCCRailway.Test.Manufacturers.NCE.VirtualPowerCabSensorTests")]

namespace DCCRailway.CmdStation.Virtual.Commands;

[Command("SensorGetState", "Get the state of a given Sensor")]
public class VirtualSensorGetState : VirtualCommand, ICmdSensorGetState, IAccyCmd {
    public override string      ToString() => $"GET SENSOR STATE ({Address.Address})";
    public          IDCCAddress Address    { get; set; }
}