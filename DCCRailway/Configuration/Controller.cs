namespace DCCRailway.Configuration;

public class Controller : Base.ConfigBase {
    public Adapter Adapter              { get; set; }
    public bool    SendStopOnDisconnect { get; set; }
}