using DCCRailway.System.Layout.Adapters;
using DCCRailway.System.Layout.Commands.Results;

namespace DCCRailway.System.Layout.Commands;

public interface ICommand {
    public ICommandResult       Execute     (IAdapter adapter);
    public Task<ICommandResult> ExecuteAsync(IAdapter adapter);
}