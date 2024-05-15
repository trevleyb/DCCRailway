using System;
using DCCRailway.Common.Helpers;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.Tasks;

namespace DCCRailway.Controller.Virtual.Tasks;

[Task("VirtualDummyTask", "Background Dummy to Poll the Console")]
public class VirtualDummyTask : ControllerTask {
    private int counter;

    protected override void OnWorkStarted() {
        Logger.Log.Debug($"Work has Started for task '{Name}'");
        base.OnWorkStarted();
    }

    protected override void OnWorkFinished() {
        Logger.Log.Debug($"Work has Finished for task '{Name}'");
        base.OnWorkFinished();
    }

    protected override void OnWorkInProgress() {
        Logger.Log.Debug($"Work is in progress for task '{Name}'");
        base.OnWorkInProgress();
    }

    protected override void DoWork() {
        counter++;
        Logger.Log.Debug($"Virtual Dummy Task: {Name}. Called {counter} times.");
    }

    protected override void Setup() {
        counter   = 0;
        Frequency = new TimeSpan(0, 0, 0, 0, 500);
    }

    protected override void CleanUp() {
        // Do Nothing
    }
}