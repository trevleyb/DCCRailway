namespace DCCRailway.Core.Commands {
	public interface IResultError : IResult {
		public string Error { get; }
	}
}