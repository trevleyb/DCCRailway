using DCCRailway.Layout.Entities;
using DCCRailway.Railway.Configuration;
using DCCRailway.Railway.Layout;
using DCCRailway.Railway.Layout.State;
using DCCRailway.Railway.Throttles.WiThrottle;
using Serilog;
using Serilog.Core;

namespace DCCRailway.Railway;

/// <summary>
///     IRailwayConfig represents the data used to manage the railway. This will include Locomotices, Blocks,
///     Switches, Tracks, Signals etc. This is within a interface so that we can store the data in different
///     formats or styles (default is a .json file).
/// </summary>
public interface IRailwayManager {

    public string        Name          { get; }
    public string        Description   { get; }
    public string        PathName      { get; }

    public ILogger       Logger        { get;}
    public Settings      Settings      { get; }

    public Accessories   Accessories   { get; }
    public Blocks        Blocks        { get; }
    public Locomotives   Locomotives   { get; }
    public Routes        Routes        { get; }
    public Sensors       Sensors       { get; }
    public Signals       Signals       { get; }
    public Turnouts      Turnouts      { get; }
    public Manufacturers Manufacturers { get; }

    public CommandStationManager CommandStationManager { get; }
    public StateManager          StateManager          { get; }
    public StateEventProcessor   StateProcessor        { get; }

    public void Start();
    public void Stop();
    public void Save();
    public IRailwayManager Load(string path, string name);
    public IRailwayManager New (string path, string name);
}