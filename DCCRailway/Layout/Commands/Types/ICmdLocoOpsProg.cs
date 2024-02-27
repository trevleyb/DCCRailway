using DCCRailway.Layout.Commands.Types.BaseTypes;
using DCCRailway.Layout.Types;

namespace DCCRailway.Layout.Commands.Types;

public interface ICmdLocoOpsProg : ICommand, ILocoCmd {
    public byte        Value       { get; set; }
}