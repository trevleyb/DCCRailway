using DCCRailway.CmdStation.Commands.Types.Base;

namespace DCCRailway.CmdStation.Commands.Types;

public interface ICmdLocoSetFunction : ICommand, ILocoCmd {
    public byte Function { get; set; }
    public bool State    { get; set; }
}