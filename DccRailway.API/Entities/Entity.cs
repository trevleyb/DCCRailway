namespace DccRailway.API.Entities {
	public abstract record Entity {
		public Guid Id { get; init; }
		public string Name { get; init; }
	}
}