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
    public string          Name            { get; }
    public string          Description     { get; }
    public string          PathName        { get; }
    public Controller      Controller      { get; }
    public WiThrottlePrefs WiThrottlePrefs { get; }

    // Collection/Repository Properties
    // -----------------------------------------------------------------------------
    public Settings      Settings      { get; }
    public Accessories   Accessories   { get; }
    public Blocks        Blocks        { get; }
    public Locomotives   Locomotives   { get; }
    public Routes        Routes        { get; }
    public Sensors       Sensors       { get; }
    public Signals       Signals       { get; }
    public Turnouts      Turnouts      { get; }
    public Manufacturers Manufacturers { get; }

    public IRailwaySettings Save();
    public IRailwaySettings Sample(string path, string name);
    public IRailwaySettings Load(string path, string name);
    public IRailwaySettings New(string path, string name);
}