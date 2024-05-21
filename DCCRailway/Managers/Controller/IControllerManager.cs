using DCCRailway.Controller.Controllers;

namespace DCCRailway.Managers.Controller;

public interface IControllerManager {
    void Start();
    void Stop();
    ICommandStation CommandStation { get; }
}