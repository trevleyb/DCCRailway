namespace DccRailway.API.Entities {
	public record Loco : Entity {
		public string Description { get; set; }
		public string Type { get; set; }
		public string RoadName { get; set; }
		public string RoadNumber { get; set; }
		public string Manufacturer { get; set; }

		public string Model { get; set; }

		//public Decoder Decoder { get; set; }
		//public Parameters Parameters { get; set; }
	}
}