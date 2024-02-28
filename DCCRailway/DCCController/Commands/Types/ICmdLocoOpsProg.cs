using DCCRailway.DCCController.Commands.Types.BaseTypes;

namespace DCCRailway.DCCController.Commands.Types;

public interface ICmdLocoOpsProg : ICommand, ILocoCmd {
    public byte        Value       { get; set; }
}