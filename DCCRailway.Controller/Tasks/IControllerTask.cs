using DCCRailway.Common.Parameters;
using DCCRailway.Controller.Controllers;
using DCCRailway.Controller.Tasks.Events;

namespace DCCRailway.Controller.Tasks;

public interface IControllerTask : IParameterMappable {
    string          Name           { get; set; }
    TimeSpan        Frequency      { get; set; }
    ICommandStation CommandStation { get; }
    int             Milliseconds   { get; }
    int             Seconds        { get; set; }

    event EventHandler<ITaskEvent> TaskEvent;

    void Start();
    void Stop();
}