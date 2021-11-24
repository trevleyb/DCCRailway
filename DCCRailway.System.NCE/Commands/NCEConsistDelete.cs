﻿using DCCRailway.Core.Adapters;
using DCCRailway.Core.Commands;
using DCCRailway.Core.Common;
using DCCRailway.Core.Utilities;

namespace DCCRailway.Systems.NCE.Commands {
	public class NCEConsistDelete : NCECommandBase, ICmdConsistDelete, ICommand {
		public NCEConsistDelete() { }

		public NCEConsistDelete(IDCCLoco loco) : this(loco.Address) { }

		public NCEConsistDelete(IDCCAddress address) {
			Address = address;
		}

		public IDCCAddress Address { get; set; }

		public static string Name {
			get { return "NCE Consist Delete"; }
		}

		public override IResult Execute(IAdapter adapter) {
			byte[] command = { 0xA2 };
			command = command.AddToArray(Address.AddressBytes);
			command = command.AddToArray(0x10);
			command = command.AddToArray(0);
			return SendAndReceieve(adapter, new NCEStandardValidation(), command);
		}

		public override string ToString() {
			return $"CONSIST DELETE ({Address})";
		}
	}
}