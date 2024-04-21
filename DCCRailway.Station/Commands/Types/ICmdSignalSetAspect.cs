using DCCRailway.Station.Commands.Types.Base;

namespace DCCRailway.Station.Commands.Types;

public interface ICmdSignalSetAspect : ICommand, ISignalCmd {
    public byte Aspect { get; set; }
    public bool Off    { set; }
}