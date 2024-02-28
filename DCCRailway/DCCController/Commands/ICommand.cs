using DCCRailway.DCCController.Adapters;
using DCCRailway.DCCController.Commands.Results;

namespace DCCRailway.DCCController.Commands;

public interface ICommand {
    public ICommandResult       Execute     (IAdapter adapter);
    public Task<ICommandResult> ExecuteAsync(IAdapter adapter);
}