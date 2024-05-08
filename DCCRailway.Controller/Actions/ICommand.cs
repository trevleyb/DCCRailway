using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Helpers;

namespace DCCRailway.Controller.Actions;

public interface ICommand : IParameterMappable {
    public ICmdResult       Execute();
    public ICmdResult       Execute(IAdapter adapter);
    public Task<ICmdResult> ExecuteAsync();
    public Task<ICmdResult> ExecuteAsync(IAdapter adapter);
    public IAdapter? Adapter { get; set; }
}