using DCCRailway.Layout.Entities;
using DCCRailway.Railway.Configuration;
using DCCRailway.Railway.Layout;
using DCCRailway.Railway.Layout.State;
using DCCRailway.Railway.Throttles.WiThrottle;

namespace DCCRailway.Railway;

/// <summary>
///     IRailwayConfig represents the data used to manage the railway. This will include Locomotices, Blocks,
///     Switches, Tracks, Signals etc. This is within a interface so that we can store the data in different
///     formats or styles (default is a .json file).
/// </summary>
public interface IRailwayManager {
    public string Name        { get; set; }
    public string Description { get; set; }
    public string PathName    { get; set; }

    public Settings Settings { get; }

    public Accessories   Accessories   { get; set; }
    public Blocks        Blocks        { get; set; }
    public Locomotives   Locomotives   { get; set; }
    public Routes        Routes        { get; set; }
    public Sensors       Sensors       { get; set; }
    public Signals       Signals       { get; set; }
    public Turnouts      Turnouts      { get; set; }
    public Manufacturers Manufacturers { get; set; }

    public CommandStationManager CommandStationManager { get; }
    public StateManager          StateManager          { get; }
    public StateEventProcessor   StateProcessor        { get; }

    public WiThrottlePreferences WiThrottlePreferences { get; }

    public void Start();
    public void Stop();
    public void Save();
}