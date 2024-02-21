using DCCRailway.Layout.Types;

namespace DCCRailway.Layout.Commands.Types;

public interface ICmdLocoSetFunctions : ICommand,ICmdWithAddress {
    public DCCFunctionBlocks Functions { get; }
}