using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions.Commands.Base;

namespace DCCRailway.Controller.Actions.Commands;

public interface ICmdConsistAdd : ICommand, IConsistCmd {
    public byte               ConsistAddress { get; set; }
    public DCCAddress         Loco           { get; set; }
    public DCCConsistPosition Position       { get; set; }
}