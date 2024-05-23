using DCCRailway.Common.Helpers;
using DCCRailway.Common.Parameters;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.Controllers;
using Serilog;

namespace DCCRailway.Controller.Tasks;

[Task("Controler Task")]
public abstract class ControllerTask(ILogger logger)
    : BackgroundWorker(logger, null), IControllerTask, IParameterMappable {
    public override void Start() {
        Setup();
        base.Start();
    }

    public override void Stop() {
        base.Stop();
        CleanUp();
    }

    public            ICommandStation CommandStation { get; set; }
    protected virtual void            Setup()        { }
    protected virtual void            CleanUp()      { }
}