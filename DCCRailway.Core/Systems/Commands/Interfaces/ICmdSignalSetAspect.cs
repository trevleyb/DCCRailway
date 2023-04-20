using DCCRailway.Core.Systems.Types;

namespace DCCRailway.Core.Systems.Commands.Interfaces; 

public interface ICmdSignalSetAspect : ICommand {
    public IDCCAddress Address { get; set; }
    public byte Aspect { get; set; }
    public bool Off { set; }
}