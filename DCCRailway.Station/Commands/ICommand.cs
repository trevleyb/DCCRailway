using DCCRailway.Station.Adapters.Base;
using DCCRailway.Station.Commands.Results;
using DCCRailway.Station.Controllers;
using DCCRailway.Station.Helpers;

namespace DCCRailway.Station.Commands;

public interface ICommand : IParameterMappable {
    public ICommandResult       Execute();
    public ICommandResult       Execute(IAdapter adapter);
    public Task<ICommandResult> ExecuteAsync();
    public Task<ICommandResult> ExecuteAsync(IAdapter adapter);

    public IAdapter? Adapter { get; set; }
}