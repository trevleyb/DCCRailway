using DCCRailway.Common.Configuration;
using DCCRailway.Common.Entities;

namespace DCCRailway.Common;

/// <summary>
///     IRailwayConfig represents the data used to manage the railway. This will include Locomotices, Blocks,
///     Switches, Tracks, Signals etc. This is within a interface so that we can store the data in different
///     formats or styles (default is a .json file).
/// </summary>
public interface IRailwaySettings {
    // Helper Properties
    // -----------------------------------------------------------------------------
    string          Name            { get; }
    string          Description     { get; }
    string          PathName        { get; }
    Controller      Controller      { get; }
    Controller      Analyser        { get; }
    WiThrottlePrefs WiThrottlePrefs { get; }

    int WebApiPort { get; }
    int WebAppPort { get; }

    // Collection/Repository Properties
    // -----------------------------------------------------------------------------
    Settings      Settings      { get; }
    Accessories   Accessories   { get; }
    Blocks        Blocks        { get; }
    Locomotives   Locomotives   { get; }
    TrackRoutes   TrackRoutes   { get; }
    Sensors       Sensors       { get; }
    Signals       Signals       { get; }
    Turnouts      Turnouts      { get; }
    Manufacturers Manufacturers { get; }

    IRailwaySettings Save();
    IRailwaySettings CreateSampleData(string path, string name);
    IRailwaySettings Load(string path, string name);
    IRailwaySettings New(string path, string name);
}