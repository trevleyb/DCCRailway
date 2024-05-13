using System;
using System.ComponentModel.DataAnnotations;
using DCCRailway.Common.Helpers;
using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.NCE.Actions.Commands;
using DCCRailway.Controller.NCE.Actions.Results;
using DCCRailway.Controller.Tasks;

namespace DCCRailway.Controller.Virtual.Tasks;

[Task("AIUPoller","NCE AIU Poller")]
public class NCEAIUPoller : ControllerTask {

    [Range(1,63)]
    public byte CabAddress { get; set; }

    protected override void Setup() {
        if (CabAddress < 1 || CabAddress > 63) throw new Exception("CAB Address must be in the range of 0 ... 63");
        base.Setup();
    }

    /// <summary>
    /// For the AIU, we need to poll the CAB fo information on the AIU device and to return
    /// the current state of the device.
    /// </summary>
    protected override void DoWork() {
        if (CommandStation.CreateCommand<ICmdSensorGetState>() is NCESensorGetState command) {
            command.SetAddressByCab(CabAddress);
            if (command.Execute() is NCECmdResultSensorState result) {

            }
        }
    }
}