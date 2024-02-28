using DCCRailway.DCCLayout.Entities.Base;

namespace DCCRailway.DCCLayout;

public class Controller : ConfigBase {
    public Adapter Adapter              { get; set; }
    public bool    SendStopOnDisconnect { get; set; }
}