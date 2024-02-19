using DCCRailway.Layout.Adapters;
using DCCRailway.Layout.Commands.Results;

namespace DCCRailway.Layout.Commands;

public interface ICommand {
    public ICommandResult       Execute     (IAdapter adapter);
    public Task<ICommandResult> ExecuteAsync(IAdapter adapter);
}