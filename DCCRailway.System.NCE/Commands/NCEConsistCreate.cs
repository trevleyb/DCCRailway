using System.Collections.Generic;
using System.Text;
using DCCRailway.Core.Adapters;
using DCCRailway.Core.Commands;
using DCCRailway.Core.Common;

namespace DCCRailway.Systems.NCE.Commands {
	public class NCEConsistCreate : NCECommandBase, ICmdConsistCreate, ICommand {
		public byte ConsistAddress { get; set; }
		public IDCCLoco LeadLoco { get; set; }
		public IDCCLoco RearLoco { get; set; }
		public List<IDCCLoco> AddLoco { get; } = new();

		public static string Name {
			get { return "NCE Consist Create"; }
		}

		public override IResult Execute(IAdapter adapter) {
			IResult result;

			// Start by deleting the current Consist by killing the consist by the lead loco
			// -----------------------------------------------------------------------------
			var killCmd = new NCEConsistKill(LeadLoco);
			result = killCmd.Execute(adapter);
			if (!result.OK) return result;

			// Add each loco to the consist
			// -----------------------------------------------------------------------------
			result = AddLocoToConsist(adapter, ConsistAddress, LeadLoco, DCCConsistPosition.Front);
			if (!result.OK) return result;

			result = AddLocoToConsist(adapter, ConsistAddress, RearLoco, DCCConsistPosition.Rear);
			if (!result.OK) return result;

			foreach (var extraLoco in AddLoco) {
				result = AddLocoToConsist(adapter, ConsistAddress, RearLoco, DCCConsistPosition.Middle);
				if (!result.OK) return result;
			}

			return new ResultOK();
		}

		private static IResult AddLocoToConsist(IAdapter adapter, byte consistAddress, IDCCLoco loco, DCCConsistPosition position) {
			// First Delete the loco from any existing Consist
			// -----------------------------------------------
			var delCmd = new NCEConsistDelete(loco);
			var delRes = delCmd.Execute(adapter);
			if (!delRes.OK) return delRes;

			var addCmd = new NCEConsistAdd(consistAddress, loco, position);
			var addRes = addCmd.Execute(adapter);
			if (!addRes.OK) return addRes;

			return new ResultOK();
		}

		public override string ToString() {
			var sb = new StringBuilder();
			foreach (var loco in AddLoco) {
				sb.Append(loco + ",");
			}
			return $"CREATE CONSIST ({ConsistAddress}={LeadLoco},{sb}{RearLoco}";
		}
	}
}