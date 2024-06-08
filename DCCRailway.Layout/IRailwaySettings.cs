using DCCRailway.Layout.Configuration;
using DCCRailway.Layout.Entities;

namespace DCCRailway.Layout;

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

    // Collection/Repository Properties
    // -----------------------------------------------------------------------------
    Settings      Settings      { get; }
    Accessories   Accessories   { get; }
    Blocks        Blocks        { get; }
    Locomotives   Locomotives   { get; }
    Routes        Routes        { get; }
    Sensors       Sensors       { get; }
    Signals       Signals       { get; }
    Turnouts      Turnouts      { get; }
    Manufacturers Manufacturers { get; }

    IRailwaySettings Save();
    IRailwaySettings Sample(string path, string name);
    IRailwaySettings Load(string path, string name);
    IRailwaySettings New(string path, string name);
}