using DCCRailway.Layout.Entities;
using DCCRailway.Railway.Configuration.Entities;
using DCCRailway.Railway.Configuration.Helpers;
using DCCRailway.Railway.Layout;
using DCCRailway.Railway.Layout.State;
using DCCRailway.Railway.Throttles.WiThrottle;
using Parameters = DCCRailway.Layout.Entities.Parameters;

namespace DCCRailway.Railway.Configuration;

/// <summary>
/// IRailwayConfig represents the data used to manage the railway. This will include Locomotices, Blocks,
/// Switches, Tracks, Signals etc. This is within a interface so that we can store the data in different
/// formats or styles (default is a .json file).
/// </summary>
public interface IRailwayManager {
    public string         Name                  { get; }
    public string         Description           { get; }
    public string         Filename              { get; }

    public Settings       Settings { get; }

    public Accessories    Accessories { get; set; }
    public Blocks         Blocks { get; set; }
    public Locomotives    Locomotives { get; set; }
    public Routes         Routes { get; set; }
    public Sensors        Sensors { get; set; }
    public Signals        Signals { get; set; }
    public Turnouts       Turnouts { get; set; }
    public Manufacturers            Manufacturers { get; set; }

    public CommandStationManager    CommandStationManager { get; }
    public StateManager             StateManager          { get; }
    public StateEventProcessor      StateProcessor        { get; }

    public WiThrottlePreferences    WiThrottlePreferences { get; }

    public void Start();
    public void Stop();
    public void Save();
    public void Save(string name);
}