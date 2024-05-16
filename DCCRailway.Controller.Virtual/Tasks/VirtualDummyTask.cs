using System;
using DCCRailway.Common.Helpers;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.Tasks;
using Serilog;
using Serilog.Core;

namespace DCCRailway.Controller.Virtual.Tasks;

[Task("VirtualDummyTask", "Background Dummy to Poll the Console")]
public class VirtualDummyTask(ILogger logger) : ControllerTask(logger) {
    private int counter;

    protected override void OnWorkStarted() {
        logger.Debug($"Work has Started for task '{Name}'");
        base.OnWorkStarted();
    }

    protected override void OnWorkFinished() {
        logger.Debug($"Work has Finished for task '{Name}'");
        base.OnWorkFinished();
    }

    protected override void OnWorkInProgress() {
        logger.Debug($"Work is in progress for task '{Name}'");
        base.OnWorkInProgress();
    }

    protected override void DoWork() {
        counter++;
        logger.Debug($"Virtual Dummy Task: {Name}. Called {counter} times.");
    }

    protected override void Setup() {
        counter   = 0;
        Frequency = new TimeSpan(0, 0, 0, 0, 500);
    }

    protected override void CleanUp() {
        // Do Nothing
    }
}