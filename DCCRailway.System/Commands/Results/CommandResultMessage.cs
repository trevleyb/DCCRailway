using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.Results;

public class CommandResultMessage {
    
    public CommandResultMessage(DCCAddress? address, string message) {
        Address = address;
        Message = message;
    }
    public DCCAddress? Address { get; }
    public string     Message { get; }
}