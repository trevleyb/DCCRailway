using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Commands.Results;
using DCCRailway.CmdStation.Helpers;
using DCCRailway.CmdStation.Controllers;

namespace DCCRailway.CmdStation.Commands;

public interface ICommand : IParameterMappable {
    public ICommandResult       Execute();
    public ICommandResult       Execute(IAdapter adapter);
    public Task<ICommandResult> ExecuteAsync();
    public Task<ICommandResult> ExecuteAsync(IAdapter adapter);

    public IAdapter? Adapter { get; set; }
}