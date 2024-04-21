using DCCRailway.Station.Adapters.Base;
using DCCRailway.Station.Commands.Results;

namespace DCCRailway.Station.Commands;

public interface ICommand {
    public ICommandResult       Execute(IAdapter      adapter);
    public Task<ICommandResult> ExecuteAsync(IAdapter adapter);
}