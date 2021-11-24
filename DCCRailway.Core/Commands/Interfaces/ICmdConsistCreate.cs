using System.Collections.Generic;
using DCCRailway.Core.Common;

namespace DCCRailway.Core.Commands {
	public interface ICmdConsistCreate : ICommand {
		public byte ConsistAddress { get; set; }
		public IDCCLoco LeadLoco { get; set; }
		public IDCCLoco RearLoco { get; set; }
		public List<IDCCLoco> AddLoco { get; }
	}
}