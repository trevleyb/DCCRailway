using DCCRailway.Layout.Entities.Base;

namespace DCCRailway.Layout;

public class Controller : ConfigBase {
    public Adapter Adapter              { get; set; }
    public bool    SendStopOnDisconnect { get; set; }
}