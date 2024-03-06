using DCCRailway.Common.Types;
using DCCRailway.System.Commands.Types.Base;

namespace DCCRailway.System.Commands.Types;

public interface ICmdLocoSetFunction : ICommand, ILocoCmd {
    public byte Function { get; set; }
    public bool State    { get; set; }
}