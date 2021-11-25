using System;
using System.Collections.Generic;
using System.Net;
using Makaretu.Dns;

namespace DCCRailway.Server.Utilities {
	public class ServerBroadcast {
		public static void Start(string instance, string service, IPAddress ipAddress, ushort port, Dictionary<string, string>? properties = null) {
			var host = Dns.GetHostEntry(Dns.GetHostName());
			if (host != null && host.AddressList.Length > 0) // Start service discovery for tyhis service.
				// -------------------------------------------------------------
			{
				try {
					var sd = new ServiceDiscovery();
					sd.AnswersContainsAdditionalRecords = true;

					//sd.ServiceInstanceShutdown += Sd_ServiceInstanceShutdown;

					var sp = new ServiceProfile("JMRI WiThrottle Railway", "_withrottle._tcp", port, host.AddressList);
					sp.AddProperty("node", "jmri-C4910CB13C68-3F39938d");
					sp.AddProperty("jmri", "4.21.4");
					sp.AddProperty("version", "4.2.1");

					//sp.AddProperty ("Version", "0.0.0.1");
					//sp.AddProperty ("Server", "DCCRailway");
					sd.AnswersContainsAdditionalRecords = true;
					sd.Advertise(sp);
				} catch (Exception ex) {
					throw new ApplicationException("Could not start Broadcast", ex);
				}
			} else
				throw new ApplicationException("Could not Broadcast since cannot determin elocal IP Addresses.");
		}

		private void Sd_ServiceInstanceShutdown(object? sender, ServiceInstanceShutdownEventArgs e) {
			DCCRailway.Core.Utilities.Logger.Log.Debug("SD: Shutdown=>{0}", e.Message);
		}

		private void Sd_ServiceInstanceDiscovered(object? sender, ServiceInstanceDiscoveryEventArgs e) {
			DCCRailway.Core.Utilities.Logger.Log.Debug("SD: Instance Discovered=>{0}", e.Message);
		}

		private void Sd_ServiceDiscovered(object? sender, DomainName e) {
			DCCRailway.Core.Utilities.Logger.Log.Debug("SD: Service Discovered=>{0}", e.Labels);
		}
	}
}