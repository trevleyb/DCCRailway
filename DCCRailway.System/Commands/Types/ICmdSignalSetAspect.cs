using DCCRailway.System.Commands.Types.Base;

namespace DCCRailway.System.Commands.Types;

public interface ICmdSignalSetAspect : ICommand, ISignalCmd {
    public byte Aspect { get; set; }
    public bool Off    { set; }
}