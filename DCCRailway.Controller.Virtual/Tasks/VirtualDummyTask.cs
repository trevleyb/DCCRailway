using System;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.Controllers;
using DCCRailway.Controller.Tasks;
using Serilog;

namespace DCCRailway.Controller.Virtual.Tasks;

[Task("VirtualDummyTask", "Background Dummy to Poll the Console")]
public class VirtualDummyTask(ILogger logger, ICommandStation cmdStation) : ControllerTask(logger, cmdStation) {
    private readonly ILogger _logger = logger;
    private          int     _counter;

    protected override void OnWorkStarted() {
        _logger.Debug($"Work has Started for task '{Name}'");
        base.OnWorkStarted();
    }

    protected override void OnWorkFinished() {
        _logger.Debug($"Work has Finished for task '{Name}'");
        base.OnWorkFinished();
    }

    protected override void OnWorkInProgress() {
        _logger.Debug($"Work is in progress for task '{Name}'");
        base.OnWorkInProgress();
    }

    protected override void DoWork() {
        _counter++;
        _logger.Debug($"Virtual Dummy Task: {Name}. Called {_counter} times.");
    }

    protected override void Setup() {
        _counter  = 0;
        Frequency = new TimeSpan(0, 0, 0, 0, 500);
    }

    protected override void CleanUp() {
        // Do Nothing
    }
}