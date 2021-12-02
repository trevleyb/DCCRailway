namespace DCCRailway.Core.Systems.Commands.Results {
	public interface IResultError : IResult {
		public string Error { get; }
	}
}