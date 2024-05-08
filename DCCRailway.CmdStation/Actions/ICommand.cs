using DCCRailway.CmdStation.Actions.Results;
using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Helpers;

namespace DCCRailway.CmdStation.Actions;

public interface ICommand : IParameterMappable {
    public ICmdResult       Execute();
    public ICmdResult       Execute(IAdapter adapter);
    public Task<ICmdResult> ExecuteAsync();
    public Task<ICmdResult> ExecuteAsync(IAdapter adapter);
    public IAdapter? Adapter { get; set; }
}