using DCCRailway.Common.Parameters;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Controllers;

namespace DCCRailway.Controller.Actions;

public interface ICommand : IParameterMappable {
    IAdapter        Adapter        { set; }
    ICommandStation CommandStation { set; }
    ICmdResult      Execute();

    //public Task<ICmdResult> ExecuteAsync();
    //public Task<ICmdResult> ExecuteAsync(IAdapter adapter);
}