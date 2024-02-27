using DCCRailway.Configuration.Entities.Base;

namespace DCCRailway.Configuration;

public class Controller : ConfigBase {
    public Adapter Adapter              { get; set; }
    public bool    SendStopOnDisconnect { get; set; }
}