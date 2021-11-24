namespace DCCRailway.Core.Common {
	public interface IDCCLoco {
		IDCCAddress Address { get; set; }
		DCCDirection Direction { get; set; }
	}
}