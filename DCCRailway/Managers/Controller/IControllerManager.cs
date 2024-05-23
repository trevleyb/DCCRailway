using DCCRailway.Controller.Controllers;

namespace DCCRailway.Managers.Controller;

public interface IControllerManager {
    ICommandStation CommandStation { get; }
    void            Start();
    void            Stop();
}