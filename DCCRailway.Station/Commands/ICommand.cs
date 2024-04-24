using DCCRailway.Station.Adapters.Base;
using DCCRailway.Station.Commands.Results;
using DCCRailway.Station.Helpers;

namespace DCCRailway.Station.Commands;

public interface ICommand : IParameterMappable {
    public ICommandResult       Execute(IAdapter      adapter);
    public Task<ICommandResult> ExecuteAsync(IAdapter adapter);
}