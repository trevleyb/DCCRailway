using DCCRailway.Common.Parameters;
using DCCRailway.Controller.Controllers;

namespace DCCRailway.Controller.Tasks;

public interface IControllerTask : IParameterMappable {
    string             Name           { get; set; }
    TimeSpan           Frequency      { get; set; }
    ICommandStation    CommandStation { get; set; }
    int                Milliseconds   { get; }
    decimal            Seconds        { get; }
    event EventHandler WorkStarted;
    event EventHandler WorkFinished;
    event EventHandler WorkInProgress;

    void Start();
    void Stop();
}