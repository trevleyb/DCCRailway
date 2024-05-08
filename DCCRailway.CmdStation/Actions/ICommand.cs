using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Commands.Results;
using DCCRailway.CmdStation.Helpers;
using DCCRailway.CmdStation.Controllers;

namespace DCCRailway.CmdStation.Commands;

public interface ICommand : IParameterMappable {
    public ICmdResult       Execute();
    public ICmdResult       Execute(IAdapter adapter);
    public Task<ICmdResult> ExecuteAsync();
    public Task<ICmdResult> ExecuteAsync(IAdapter adapter);
    public IAdapter? Adapter { get; set; }
}