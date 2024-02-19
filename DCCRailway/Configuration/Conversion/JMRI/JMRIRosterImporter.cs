using DCCRailway.Configuration.Entities;
using DCCRailway.Layout.Types;
using DCCRailway.Utilities;

namespace DCCRailway.Configuration.Conversion.JMRI;

public static class JMRIRosterImporter  {
    public static Locomotives Import(string rosterName) {
        if (!File.Exists(rosterName)) throw new ApplicationException($"Could not find the file '{rosterName}' in '{Directory.GetCurrentDirectory()}'");

        // First attempt to load the existing roster file from JMRI
        // ---------------------------------------------------------
        try {
            var jmriRoster = JMRIRoster.Load(rosterName);
            if (jmriRoster == null) return new Locomotives();
            return MapJMRItoDCCTrain(jmriRoster);
        } catch (Exception ex) {
            throw new Exception($"Unable to load the current JMRI Roster file '{rosterName}' due to '{ex.Message}'");
        }
    }

    /// <summary>
    ///     Mapp the Locomotives from JMRI to the DCCRailway.Delete Format
    /// </summary>
    /// <param name="locoList">Collection of Locomotives</param>
    /// <param name="jMRIRoster">The JMRI Roster File</param>
    private static Locomotives MapJMRItoDCCTrain(JMRIRoster jmriRoster) {
        Locomotives locoList = new();
        var manufacturers = new Manufacturers();

        foreach (var jmri in jmriRoster.Roster.JMRILocos) {
            try {
                var loco = new Entities.Locomotive {
                    Name         = (jmri.RoadName + ' ' + jmri.RoadNumber).Trim(),
                    Description  = jmri.Comment,
                    Type         = "unknown",
                    RoadName     = jmri.RoadName,
                    RoadNumber   = jmri.RoadNumber,
                    Manufacturer = jmri.Mfg,
                    Model        = jmri.Model
                };

                loco.Decoder.Manufacturer = manufacturers.FindByManufacturer(jmri.Mfg);
                loco.Decoder.Model        = jmri.Decoder.Model;
                loco.Decoder.Family       = jmri.Decoder.Family;

                if (jmri.Locoaddress != null && jmri.Locoaddress.Dcclocoaddress != null) {
                    loco.Decoder.Address = new DCCAddress(Convert.ToUInt16(jmri.Locoaddress.Dcclocoaddress.Number));
                    loco.Decoder.AddressType = jmri.Locoaddress.Dcclocoaddress.Longaddress.Equals("yes")
                        ? DCCAddressType.Long
                        : DCCAddressType.Short;
                    if (jmri.Locoaddress.Protocol.Equals("dcc_long")) loco.Decoder.AddressType  = DCCAddressType.Long;
                    if (jmri.Locoaddress.Protocol.Equals("dcc_short")) loco.Decoder.AddressType = DCCAddressType.Short;
                    loco.Decoder.Protocol = DCCProtocol.DCC28;
                }

                locoList.Add(loco);
            }
            catch (Exception ex) {
                Logger.Log.Error("Unable to map the JMRI Locomotive to the DCCRailway.Delete format due to '{0}'", ex.Message);
            }
        }
        return locoList;
    }
}