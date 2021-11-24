using System;
using System.Collections.Generic;
using DCCRailway.Core;
using DCCRailway.Core.Adapters;
using DCCRailway.Core.Commands;
using DCCRailway.Core.Common;
using DCCRailway.Core.Events;
using DCCRailway.Core.Exceptions;
using DCCRailway.Systems.NCE.Adapters;
using DCCRailway.Systems.NCE.Commands;

namespace DCCRailway.Systems.NCE {
	public class NCEPowerCab : SystemBase, ISystem {
		public static string Manufacturer {
			get { return "NCE"; }
		}

		public static string SystemName {
			get { return "PowerCab"; }
		}

		public override List<(Type adapter, string name)>? SupportedAdapters {
			get {
				List<(Type adapter, string name)>? adapters = new();
				adapters.Add((typeof(NCESerial), NCESerial.Name));
				adapters.Add((typeof(NCEUSBSerial), NCEUSBSerial.Name));
				adapters.Add((typeof(NCEVirtualAdapter), VirtualAdapter.Name));
				return adapters;
			}
		}

		public override IDCCAddress CreateAddress() {
			return new DCCAddress();
		}

		public override IDCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long) {
			return new DCCAddress(address, type);
		}

		protected override void RegisterCommands() {
			// With the NCE Devices, it depends on the ADAPTER being used
			// as to what commands it will support
			// -----------------------------------------------------------------
			// The NCE USB Interface doesn't support all JMRI features and functions.
			// Some of the restrictions are based on the type of system the USB Adapter is connected to.
			// The USB version 6.* can't get information from AIUs, so they can't be used to get feedback from the layout.
			// The USB 7.* version when connected to a system with the 1.65 or higher firmware (PowerCab, SB5, Twin)
			// the AIU cards can be used, but with restricted cab numbers as in the system manual.
			// The turnout feedback mode MONITORING isn't available when using a USB, and the Clock functions
			// are also not available.
			// The USB when connected to a Power Pro system doesn't support any type of loco programming,
			// and when connected to a SB3 only operation mode (no program track) is available for loco programming.
			// -----------------------------------------------------------------
			if (Adapter != null) {
				Register<IDummyCmd>(typeof(NCEDummyCmd));
				Register<ICmdStatus>(typeof(NCEStatusCmd));

				Register<ICmdTrackMain>(typeof(NCESetMainTrk));
				Register<ICmdTrackProg>(typeof(NCESetProgTrk));
				Register<ICmdPowerSetOn>(typeof(NCESetMainTrk));
				Register<ICmdPowerSetOff>(typeof(NCESetProgTrk));

				Register<ICmdSensorGetState>(typeof(NCESensorGetState));
				Register<ICmdSignalSetAspect>(typeof(NCESignalSetAspect));
				Register<ICmdAccySetState>(typeof(NCEAccySetState));

				Register<ICmdLocoSetFunctions>(typeof(NCELocoSetFunctions));
				Register<ICmdLocoSetSpeed>(typeof(NCELocoSetSpeed));
				Register<ICmdLocoSetSpeedSteps>(typeof(NCELocoSetSpeedSteps));
				Register<ICmdLocoSetMomentum>(typeof(NCELocoSetMomentum));
				Register<ICmdLocoStop>(typeof(NCELocoStop));

				Register<ICmdAccyOpsProg>(typeof(NCEAccyOpsProg));
				Register<ICmdLocoOpsProg>(typeof(NCELocoOpsProg));

				Register<ICmdConsistCreate>(typeof(NCEConsistCreate));
				Register<ICmdConsistKill>(typeof(NCEConsistKill));
				Register<ICmdConsistAdd>(typeof(NCEConsistAdd));
				Register<ICmdConsistDelete>(typeof(NCEConsistDelete));

				Register<ICmdCVRead>(typeof(NCECVRead));
				Register<ICmdCVWrite>(typeof(NCECVWrite));

				Register<ICmdMacroRun>(typeof(NCEMacroRun));

				if (Adapter is NCESerial) {
					Register<ICmdClockSet>(typeof(NCESetClock));
					Register<ICmdClockRead>(typeof(NCEReadClock));
					Register<ICmdClockStart>(typeof(NCEStartClock));
					Register<ICmdClockStop>(typeof(NCEStopClock));
				} else if (Adapter is NCEUSBSerial) {
					if (CreateCommand<ICmdStatus>() is NCEStatusCmd statusCmd && statusCmd.Execute(Adapter) is IResultStatus status) {
						switch (status.Version) {
						case "6.x.x": break; // Cannot get AIU Information
						case "7.3.0": break;
						case "7.3.1": break;
						case "7.3.2": break;
						case "7.3.3": break;
						case "7.3.4": break;
						case "7.3.5": break;
						case "7.3.6": break;
						case "7.3.7": break;
						}
					} else
						throw new AdapterException(Adapter, ":Unable to communicate with the Command Station.");
				}
			}
		}

		#region Manage the events from the Adapter
		protected override void Adapter_ErrorOccurred(object? sender, ErrorArgs e) {
			Console.WriteLine(e.ToString());
		}

		protected override void Adapter_ConnectionStatusChanged(object? sender, StateChangedArgs e) {
			Console.WriteLine(e.ToString());
		}

		protected override void Adapter_DataSent(object? sender, DataSentArgs e) {
			Console.WriteLine(e.ToString());
		}

		protected override void Adapter_DataReceived(object? sender, DataRecvArgs e) {
			Console.WriteLine(e.ToString());
		}
		#endregion
	}
}