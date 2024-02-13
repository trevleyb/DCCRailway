using DCCRailway.Layout.Types;

namespace DCCRailway.Layout.Commands.Types; 

public interface IAccyCommand {
    public IDCCAddress Address { get; set; }
}