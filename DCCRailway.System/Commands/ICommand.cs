using DCCRailway.System.Adapters;
using DCCRailway.System.Commands.Result;

namespace DCCRailway.System.Commands;

public interface ICommand {
    public IResult       Execute(IAdapter      adapter);
    public Task<IResult> ExecuteAsync(IAdapter adapter);
}