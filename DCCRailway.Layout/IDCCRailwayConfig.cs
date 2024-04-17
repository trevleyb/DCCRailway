using DCCRailway.Layout.Entities;

namespace DCCRailway.Layout;

public interface IDCCRailwayConfig {
    string      Name        { get; set; }
    string      Description { get; set; }
    Controllers Controllers { get; set; }
    Parameters  Parameters  { get; set; }
    Locomotives Locomotives { get; set; }
    Turnouts    Turnouts    { get; set; }
    Signals     Signals     { get; set; }
    Sensors     Sensors     { get; set; }
    Accessories Accessories { get; set; }
    Blocks      Blocks      { get; set; }
    void        Save(string? name);
    string?     Save();
}