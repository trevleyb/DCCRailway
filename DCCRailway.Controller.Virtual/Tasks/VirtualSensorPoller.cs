using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DCCRailway.Common.Helpers;
using DCCRailway.Common.Parameters;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.Tasks;
using DCCRailway.Controller.Virtual.Actions.Commands;

namespace DCCRailway.Controller.Virtual.Tasks;

[Task("AIUPoller", "NCE AIU Poller")]
public class VirtualSensorPoller : ControllerTask, IParameterMappable {
    [Range(1, 63), Parameter("Virtual Cab Address of this AIU interface", "Should be between 1..63")]
    public byte CabAddress { get; set; }

    protected override void Setup() {
        if (CabAddress < 1 || CabAddress > 63) throw new Exception("CAB Address must be in the range of 0 ... 63");
        base.Setup();
    }

    /// <summary>
    ///     For the AIU, we need to poll the CAB fo information on the AIU device and to return
    ///     the current state of the device.
    /// </summary>
    protected override void DoWork() {
        var pinStr = new StringBuilder();
        var pins   = new bool[14];
        if (CommandStation.CreateCommand<ICmdSensorGetState>() is VirtualSensorGetState command) {
            pinStr.Append("|");
            for (byte pin = 1; pin <= 14; pin++) {
                pins[pin - 1] = new Random().Next(1, 100) < 10;
                pinStr.Append(pins[pin - 1] ? "X" : ".");
            }
            pinStr.Append("|");
            Logger.Log.Information($"Read AIU '{CabAddress}' => {pinStr}");
        }
    }
}