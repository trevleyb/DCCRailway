using DCCRailway.CmdStation.Commands.Types.Base;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Commands.Types;

public interface ICmdConsistAdd : ICommand, IConsistCmd {
    public byte               ConsistAddress { get; set; }
    public DCCAddress         Loco           { get; set; }
    public DCCConsistPosition Position       { get; set; }
}