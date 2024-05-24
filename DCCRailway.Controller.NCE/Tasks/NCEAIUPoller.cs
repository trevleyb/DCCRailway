using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DCCRailway.Common.Parameters;
using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.NCE.Actions.Commands;
using DCCRailway.Controller.NCE.Actions.Results;
using DCCRailway.Controller.Tasks;
using Serilog;

namespace DCCRailway.Controller.NCE.Tasks;

[Task("AIUPoller", "NCE AIU Poller")]
public class NCEAIUPoller(ILogger logger) : ControllerTask(logger), IParameterMappable {
    private readonly ILogger _logger = logger;

    [Range(1, 63)]
    [Parameter("NCE Cab Address of this AIU interface", "Should be between 1..63")]
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

        if (CommandStation.CreateCommand<ICmdSensorGetState>() is NCESensorGetState command) {
            pinStr.Append("|");

            for (byte pin = 1; pin <= 14; pin++) {
                command.SetAddressByCabPin(CabAddress, pin);

                if (command.Execute() is NCECmdResultSensorState result) {
                    pins[pin - 1] = result.State == DCCAccessoryState.Occupied ? true : false;
                    pinStr.Append(result.State == DCCAccessoryState.Occupied ? "X" : ".");
                }
            }

            pinStr.Append("|");
            _logger.Information($"Read AIU '{CabAddress}' => {pinStr}");
        }
    }
}