using DCCRailway.Common.Helpers;
using DCCRailway.Common.Types;
using DCCRailway.Layout.Configuration;
using DCCRailway.Layout.Configuration.Entities.Layout;
using DCCRailway.Layout.Configuration.Entities.System;

namespace DCCRailway.Layout.Conversion.JMRI;

public static class JMRIRosterImporter {
    public static void Import(string rosterName) {
        if (!File.Exists(rosterName)) throw new ApplicationException($"Could not find the file '{rosterName}' in '{Directory.GetCurrentDirectory()}'");

        // First attempt to load the existing roster file from JMRI
        // ---------------------------------------------------------
        try {
            var jmriRoster = JMRIRoster.Load(rosterName);
            if (jmriRoster == null) return;
            MapJMRItoDCCTrain(jmriRoster);
        }
        catch (Exception ex) {
            throw new Exception($"Unable to load the current JMRI Roster file '{rosterName}' due to '{ex.Message}'");
        }
    }

    /// <summary>
    ///     Mapp the Locomotives from JMRI to the DCCRailway.Delete Format
    /// </summary>
    /// <param name="locoList">Collection of Locomotives</param>
    /// <param name="jMRIRoster">The JMRI Roster File</param>
    private static void MapJMRItoDCCTrain(JMRIRoster jmriRoster) {

        var locomotiveRepository = RailwayConfig.Instance.LocomotiveRepository;
        locomotiveRepository.DeleteAll();

        foreach (var jmri in jmriRoster.Roster.JMRILocos) {
            try {
                var loco = new Locomotive() {
                    Id           = locomotiveRepository!.GetNextID().Result,
                    Name         = (jmri.RoadName + ' ' + jmri.RoadNumber).Trim(),
                    Description  = jmri.Comment,
                    Type         = "unknown",
                    RoadName     = jmri.RoadName,
                    RoadNumber   = jmri.RoadNumber,
                    Manufacturer = jmri.Mfg,
                    Model        = jmri.Model,
                    //Decoder      = new Configuration.Entities.Layout.Decoder() {
                    //    Manufacturer = RailwayConfig.Instance.ManufacturerRepository.Find(x => x.Name == jmri.Mfg),
                    //    Model = jmri.Decoder.Model,
                    //    Family = jmri.Decoder.Family
                    //}
                };

                if (jmri.Locoaddress != null && jmri.Locoaddress.Dcclocoaddress != null) {
                    loco.Address = new DCCAddress(Convert.ToUInt16(jmri.Locoaddress.Dcclocoaddress.Number));
                    loco.Address.AddressType = jmri.Locoaddress.Dcclocoaddress.Longaddress.Equals("yes")
                        ? DCCAddressType.Long
                        : DCCAddressType.Short;
                    if (jmri.Locoaddress.Protocol.Equals("dcc_long")) loco.Address.AddressType  = DCCAddressType.Long;
                    if (jmri.Locoaddress.Protocol.Equals("dcc_short")) loco.Address.AddressType = DCCAddressType.Short;
                    loco.Address.Protocol = DCCProtocol.DCC28;
                }
                if (locomotiveRepository != null) locomotiveRepository.AddAsync(loco);
            }
            catch (Exception ex) {
                Logger.Log.Error("Unable to map the JMRI Locomotive to the DCCRailway format due to '{0}'", ex.Message);
            }
        }
    }
}