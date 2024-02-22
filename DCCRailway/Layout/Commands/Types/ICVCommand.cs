using DCCRailway.Layout.Types;

namespace DCCRailway.Layout.Commands.Types; 

public interface ICVCommand {
    public int         CV      { get; }
    public IDCCAddress? Address { get; set; }
}