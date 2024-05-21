using DCCRailway.Common.Types;
using DCCRailway.Layout.Entities;
using ILogger = Serilog.ILogger;

namespace DCCRailway.Layout.Converters.JMRI;

public class JmriRosterImporter(ILogger logger) {
    public void Import(Locomotives locomotives, string rosterName) {
        if (!File.Exists(rosterName)) throw new ApplicationException($"Could not find the file '{rosterName}' in '{Directory.GetCurrentDirectory()}'");

        // First attempt to load the existing roster file from JMRI
        // ---------------------------------------------------------
        try {
            var jmriRoster = JMRIRoster.Load(rosterName);
            if (jmriRoster == null) return;
            MapJmrItoDCCTrain(locomotives, jmriRoster);
        } catch (Exception ex) {
            throw new Exception($"Unable to load the current JMRI Roster file '{rosterName}' due to '{ex.Message}'");
        }
    }

    /// <summary>
    /// Maps JMRI locomotives to DCC trains and imports them into the system.
    /// </summary>
    /// <param name="locomotives">The collection of locomotives to import to.</param>
    /// <param name="jmriRoster">The JMRI roster to map and import from.</param>
    private void MapJmrItoDCCTrain(Locomotives locomotives, JMRIRoster jmriRoster) {
        locomotives.Clear();

        foreach (var jmri in jmriRoster.Roster.JMRILocos) {
            try {
                var loco = new Locomotive {
                    Id           = locomotives.GetNextID(),
                    Name         = (jmri.RoadName + ' ' + jmri.RoadNumber).Trim(),
                    Description  = jmri.Comment,
                    RoadName     = jmri.RoadName,
                    RoadNumber   = jmri.RoadNumber,
                    Manufacturer = jmri.Mfg,
                    Model        = jmri.Model

                    //Decoder      = new Configuration.Entities.Entities.Decoder() {
                    //    Manufacturer = RailwayConfig.Instance.ManufacturerRepository.Find(x => x.Name == jmri.Mfg),
                    //    Model = jmri.Decoder.Model,
                    //    Family = jmri.Decoder.Family
                    //}
                };

                foreach (var label in jmri.Functionlabels.Functionlabel) {
                    try {
                        loco.Labels.Add(new LabelFunction() { Key = byte.Parse(label.Num), Label = label.Text });
                    } catch { /* if an error happens, ignore it */ }
                }

                loco.Address = new DCCAddress(Convert.ToUInt16(jmri.Locoaddress.Dcclocoaddress.Number)) {
                    AddressType = jmri.Locoaddress.Dcclocoaddress.Longaddress.Equals("yes")
                        ? DCCAddressType.Long
                        : DCCAddressType.Short
                };
                if (jmri.Locoaddress.Protocol.Equals("dcc_long")) loco.Address.AddressType  = DCCAddressType.Long;
                if (jmri.Locoaddress.Protocol.Equals("dcc_short")) loco.Address.AddressType = DCCAddressType.Short;
                loco.Address.Protocol = DCCProtocol.DCC28;
                locomotives.Add(loco);
            } catch (Exception ex) {
                logger.Error("Unable to map the JMRI Locomotive to the DCCRailway format due to '{0}'", ex.Message);
            }
        }
    }
}