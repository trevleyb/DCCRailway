using DCCRailway.Layout.Commands.Types.BaseTypes;
using DCCRailway.Layout.Types;

namespace DCCRailway.Layout.Commands.Types;

public interface ICmdConsistAdd : ICommand, IConsistCmd {
    public byte               ConsistAddress { get; set; }
    public DCCAddress         Loco           { get; set; }
    public DCCConsistPosition Position       { get; set; }
}