namespace DCCRailway.Core.Systems.Types; 

public interface IDCCLoco {
    IDCCAddress Address { get; set; }
    DCCDirection Direction { get; set; }
}