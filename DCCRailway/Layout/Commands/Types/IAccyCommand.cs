using DCCRailway.System.Layout.Types;

namespace DCCRailway.System.Layout.Commands.Types; 

public interface IAccyCommand {
    public IDCCAddress Address { get; set; }
}