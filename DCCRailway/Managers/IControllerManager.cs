using DCCRailway.Controller.Controllers;

namespace DCCRailway.Managers;

public interface IControllerManager {
    ICommandStation CommandStation { get; }
    void            Start();
    void            Stop();
}