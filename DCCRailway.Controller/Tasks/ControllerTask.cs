using DCCRailway.Common.Helpers;
using DCCRailway.Common.Parameters;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.Controllers;

namespace DCCRailway.Controller.Tasks;

[Task("Controler Task")]
public abstract class ControllerTask() : BackgroundWorker(null, null), IControllerTask, IParameterMappable {
    protected virtual void Setup()   { }
    protected virtual void CleanUp() { }

    public override void Start() {
        Setup();
        base.Start();
    }

    public override void Stop() {
        base.Stop();
        CleanUp();
    }

    public ICommandStation CommandStation { get; set; }
}