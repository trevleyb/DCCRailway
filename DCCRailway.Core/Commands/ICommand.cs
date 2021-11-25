using System.Threading.Tasks;
using DCCRailway.Core.Adapters;

namespace DCCRailway.Core.Commands {
	public interface ICommand {
		public static string Name { get; }
		public IResult Execute(IAdapter adapter);
		public Task<IResult> ExecuteAsync(IAdapter adapter);
	}
}