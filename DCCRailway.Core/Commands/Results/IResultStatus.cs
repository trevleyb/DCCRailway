namespace DCCRailway.Core.Commands {
	public interface IResultStatus : IResult {
		public string Version { get; }
	}
}