namespace DCCRailway.System.Layout.Types;

public interface IDCCLoco {
    IDCCAddress  Address   { get; set; }
    DCCDirection Direction { get; set; }
}