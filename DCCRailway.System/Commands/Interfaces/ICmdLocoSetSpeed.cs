using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.Interfaces;

public interface ICmdLocoSetSpeed : ICommand,ILocoCommand {
    public IDCCAddress  Address    { get; set; }
    public DCCProtocol  SpeedSteps { get; set; }
    public DCCDirection Direction  { get; set; }
    public byte         Speed      { get; set; }
}