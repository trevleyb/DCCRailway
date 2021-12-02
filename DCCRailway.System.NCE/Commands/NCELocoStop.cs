﻿using DCCRailway.Core.Common;
using DCCRailway.Core.Systems.Adapters;
using DCCRailway.Core.Systems.Commands;
using DCCRailway.Core.Systems.Commands.Interfaces;
using DCCRailway.Core.Systems.Commands.Results;
using DCCRailway.Core.Utilities;

namespace DCCRailway.Systems.NCE.Commands {
	public class NCELocoStop : NCECommandBase, ICmdLocoStop, ICommand {
		public NCELocoStop() { }

		public NCELocoStop(IDCCLoco loco) : this(loco.Address, loco.Direction) { }

		public NCELocoStop(int address, DCCDirection direction = DCCDirection.Forward) : this(new DCCAddress(address), direction) { }

		public NCELocoStop(IDCCAddress address, DCCDirection direction = DCCDirection.Forward) {
			Address = address;
			Direction = direction;
		}

		public DCCDirection Direction { get; set; }

		public IDCCAddress Address { get; set; }

		public static string Name {
			get { return "NCE Stop Loco"; }
		}

		public override IResult Execute(IAdapter adapter) {
			byte[] command = { 0xA2 };
			command = command.AddToArray(((DCCAddress)Address).AddressBytes);
			command = command.AddToArray((byte)(Direction == DCCDirection.Forward ? 0x06 : 0x05));
			command = command.AddToArray(0);
			return SendAndReceieve(adapter, new NCEStandardValidation(), command);
		}

		public override string ToString() {
			return $"LOCO STOP ({Address}={Direction}";
		}
	}
}