using DCCRailway.System.Adapters;
using DCCRailway.System.Commands.Results;

namespace DCCRailway.System.Commands;

public interface ICommand {
    public IResult Execute(IAdapter adapter);
    public Task<IResult> ExecuteAsync(IAdapter adapter);
}