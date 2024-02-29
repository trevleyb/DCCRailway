using DCCRailway.System.Commands.Types.BaseTypes;

namespace DCCRailway.System.Commands.Types;

public interface ICmdLocoOpsProg : ICommand, ILocoCmd {
    public byte Value { get; set; }
}