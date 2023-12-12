using DCCRailway.System.Adapters;
using DCCRailway.System.Commands.Results;

namespace DCCRailway.System.Commands;

public interface ICommand {
    public CommandResult       Execute     (IAdapter adapter);
    public Task<CommandResult> ExecuteAsync(IAdapter adapter);
}