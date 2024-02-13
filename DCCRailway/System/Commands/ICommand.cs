using DCCRailway.System.Adapters;
using DCCRailway.System.Commands.Results;

namespace DCCRailway.System.Commands;

public interface ICommand {
    public ICommandResult       Execute     (IAdapter adapter);
    public Task<ICommandResult> ExecuteAsync(IAdapter adapter);
}