using DCCRailway.System.Config;
using DCCRailway.System.Conversion.JMRI;
using DCCRailway.System.Types;

namespace DCCRailway.Conversion.JMRI.Roster;

public static class JMRIRosterImporter {
    public static List<Loco> Import(string rosterName) {
        if (!File.Exists(rosterName)) throw new ApplicationException($"Could not find the file '{rosterName}' in '{Directory.GetCurrentDirectory()}'");

        // First attempt to load the existing roster file from JMRI
        // ---------------------------------------------------------
        try {
            var jmriRoster = Rosterconfig.Load(rosterName);

            if (jmriRoster == null) return new List<Loco>();

            return MapJMRItoDCCTrain(jmriRoster);
        }
        catch (Exception ex) {
            throw new Exception($"Unable to load the current JMRI Roster file '{rosterName}' due to '{ex.Message}'");
        }
    }

    /// <summary>
    ///     Mapp the Locomotives from JMRI to the DCCRailway Format
    /// </summary>
    /// <param name="locoList">Collection of Locomotives</param>
    /// <param name="jMRIRoster">The JMRI Roster File</param>
    private static List<Loco> MapJMRItoDCCTrain(Rosterconfig jmriRoster) {
        List<Loco> locoList = new();
        var manufacturers = new Manufacturers();

        foreach (var jmri in jmriRoster.Roster.Locomotive) {
            var loco = new Loco {
                Id = jmri.Id,
                Name = (jmri.RoadName + ' ' + jmri.RoadNumber).Trim(),
                Description = jmri.Comment,
                Type = "unknown",
                RoadName = jmri.RoadName,
                RoadNumber = jmri.RoadNumber,
                Manufacturer = jmri.Mfg,
                Model = jmri.Model
            };

            if (jmri.Decoder != null) {
                loco.Decoder.Manufacturer = manufacturers.Find(jmri.Mfg);
                loco.Decoder.Model = jmri.Decoder.Model;
                loco.Decoder.Family = jmri.Decoder.Family;
            }

            if (jmri.Locoaddress != null && jmri.Locoaddress.Dcclocoaddress != null) {
                loco.Decoder.Address = Convert.ToUInt16(jmri.Locoaddress.Dcclocoaddress.Number);
                loco.Decoder.AddressType = jmri.Locoaddress.Dcclocoaddress.Longaddress.Equals("yes") ? DCCAddressType.Long : DCCAddressType.Short;
                if (jmri.Locoaddress.Protocol.Equals("dcc_long")) loco.Decoder.AddressType = DCCAddressType.Long;
                if (jmri.Locoaddress.Protocol.Equals("dcc_short")) loco.Decoder.AddressType = DCCAddressType.Short;
                loco.Decoder.Protocol = DCCProtocol.DCC28;
            }

            locoList.Add(loco);
        }

        return locoList;
    }
}