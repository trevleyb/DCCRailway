using DCCRailway.Controller.Controllers;
using DCCRailway.Controller.Controllers.Events;
using DCCRailway.Controller.Tasks.Events;

namespace DCCRailway.Managers;

public interface IControllerManager {
    ICommandStation?                        CommandStation { get; }
    event EventHandler<ControllerEventArgs> ControllerEvent;
    event EventHandler<ITaskEvent>          TaskEvent;
    void                                    Start();
    void                                    Stop();
}