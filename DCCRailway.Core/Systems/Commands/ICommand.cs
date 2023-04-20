using System.Threading.Tasks;
using DCCRailway.Core.Systems.Adapters;
using DCCRailway.Core.Systems.Commands.Results;

namespace DCCRailway.Core.Systems.Commands; 

public interface ICommand {
    public static string Name { get; }
    public IResult Execute(IAdapter adapter);
    public Task<IResult> ExecuteAsync(IAdapter adapter);
}