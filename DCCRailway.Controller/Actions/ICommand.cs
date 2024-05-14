using DCCRailway.Common.Parameters;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Controllers;

namespace DCCRailway.Controller.Actions;

public interface ICommand : IParameterMappable {
    ICmdResult      Execute();
    IAdapter        Adapter { set; }
    ICommandStation CommandStation { set; }

    //public Task<ICmdResult> ExecuteAsync();
    //public Task<ICmdResult> ExecuteAsync(IAdapter adapter);
}