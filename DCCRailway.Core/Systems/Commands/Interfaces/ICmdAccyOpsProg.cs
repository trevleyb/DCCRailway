﻿using DCCRailway.Core.Systems.Types;

namespace DCCRailway.Core.Systems.Commands.Interfaces {
	public interface ICmdAccyOpsProg : ICommand {
		public IDCCAddress LocoAddress { get; set; }
		public IDCCAddress CVAddress { get; set; }
		public byte Value { get; set; }
	}
}