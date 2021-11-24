namespace DCCRailway.Core.Common {
	public class DCCLoco : IDCCLoco {
		public DCCLoco(IDCCAddress address, DCCDirection direction = DCCDirection.Forward) {
			Address = address;
			Direction = direction;
		}

		public DCCLoco(int address, DCCAddressType type = DCCAddressType.Long, DCCDirection direction = DCCDirection.Forward) {
			Address = new DCCAddress(address, type);
			Direction = direction;
		}

		public IDCCAddress Address { get; set; }
		public DCCDirection Direction { get; set; }
	}
}