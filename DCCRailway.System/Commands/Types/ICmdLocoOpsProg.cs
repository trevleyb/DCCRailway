using DCCRailway.System.Commands.Types.Base;

namespace DCCRailway.System.Commands.Types;

public interface ICmdLocoOpsProg : ICommand, ILocoCmd {
    public byte Value { get; set; }
}