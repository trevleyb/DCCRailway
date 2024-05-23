using DCCRailway.Common.Parameters;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Controllers;
using Serilog;

namespace DCCRailway.Controller.Actions;

public interface ICommand : IParameterMappable {
    IAdapter        Adapter        { set; }
    ICommandStation CommandStation { set; }
    ILogger         Logger         { get; set; }
    ICmdResult      Execute();
}