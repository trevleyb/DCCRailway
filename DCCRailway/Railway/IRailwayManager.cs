using DCCRailway.Layout.Layout.Entities;
using DCCRailway.Railway.Configuration.Entities;
using DCCRailway.Railway.Configuration.Helpers;
using DCCRailway.Railway.Layout;
using DCCRailway.Railway.Layout.State;
using Parameters = DCCRailway.Layout.Layout.Entities.Parameters;

namespace DCCRailway.Railway.Configuration;

/// <summary>
/// IRailwayConfig represents the data used to manage the railway. This will include Locomotices, Blocks,
/// Switches, Tracks, Signals etc. This is within a interface so that we can store the data in different
/// formats or styles (default is a .json file).
/// </summary>
public interface IRailwayManager {
    public string         Name                  { get; set;  }
    public string         Description           { get; set; }
    public string         Filename              { get; set; }

    public DCCController  Controller            { get; }
    public Parameters     Parameters            { get; }
    public Manufacturers  Manufacturers         { get; }

    public Accessories    Accessories           { get; }
    public Blocks         Blocks                { get; }
    public Locomotives    Locomotives           { get; }
    public Routes         Routes                { get; }
    public Sensors        Sensors               { get; }
    public Signals        Signals               { get; }
    public Turnouts       Turnouts              { get; }

    public CommandStationManager    CommandStationManager { get; }
    public StateManager             StateManager          { get; }
    public StateEventProcessor      StateProcessor        { get; }

    public void Start();
    public void Stop();
    public void Save();
    public void Save(string name);
}