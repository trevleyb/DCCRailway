using DCCRailway.Common.Helpers;
using DCCRailway.Common.Parameters;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.Controllers;
using DCCRailway.Controller.Tasks.Events;
using Serilog;

namespace DCCRailway.Controller.Tasks;

[Task("Controler Task")]
public abstract class ControllerTask(ILogger logger, ICommandStation cmdStation) : BackgroundWorker(logger, null), IControllerTask, IParameterMappable {
    public event EventHandler<ITaskEvent> TaskEvent;

    public override void Start() {
        Setup();
        base.Start();
        OnTaskEvent(this, new TaskStartEvent(this));
    }

    public override void Stop() {
        base.Stop();
        CleanUp();
        OnTaskEvent(this, new TaskStopEvent(this));
    }

    public            ICommandStation CommandStation { get; } = cmdStation;
    protected virtual void            Setup()        { }
    protected virtual void            CleanUp()      { }

    protected void OnTaskEvent(object sender, ITaskEvent e) {
        TaskEvent?.Invoke(sender, e);
    }
}