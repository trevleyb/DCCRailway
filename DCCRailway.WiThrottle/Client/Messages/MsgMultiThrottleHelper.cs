using DCCRailway.Common.Types;

namespace DCCRailway.WiThrottle.Client.Messages;

/// <summary>
///     Class that breaks up a command string into the 3 consituent parts ensuring that the data is also valid.
/// </summary>
public class MsgMultiThrottleHelper {
    private static readonly string Delimiter      = "<;>";
    private static readonly char[] ValidFunctions = ['+', '-', 'S', 'A', 'L'];

    public MsgMultiThrottleHelper(string commandStr) {
        //
        // The command is always made up in the following format(s)
        // 1. [3 char command][address]<;>[action]
        // 2. 3 charcter command is always M[G][Function]
        // 3. G is the Group so we can group locos
        // 4. Function must be one of + - S A L
        // ----------------------------------------------------------------------------------------------------------
        // Normally a message is sent with the address to control such as MT+L5678 which is Loco 5678(long)
        // But some controllers, for example the TCS UWT-100, sends a * as a "for all my locos" so we need to
        // cater for this. So we don't return a single ADDRESS for a command but an array which most of the time 
        // will only have a single address, but could contain multiple addresses so all code that performs an action
        // for a loco/address needs to iterate and perform the action for as many locos as there are in the array. 
        // The array is populated if a * is used by the current assigned locos for the connection and group. 
        // ----------------------------------------------------------------------------------------------------------

        Message  = commandStr;
        Function = ThrottleFunctionEnum.AcquireLoco;

        try {
            if (commandStr[0] != 'M') throw new Exception("Invalid Multi-Throttle Command.");
            Group    = commandStr[1];
            Function = (ThrottleFunctionEnum)commandStr[2];

            var delimiterPos = commandStr.IndexOf(Delimiter, StringComparison.Ordinal);
            if (delimiterPos == -1 || delimiterPos == 0 || delimiterPos + 1 > commandStr.Length) {
                throw new Exception("Invalid Multi-Throttle Message");
            }

            // Check the address. Can either be * (all in the group) or L or S followed by the DCC Address
            var addressStr = commandStr[3..delimiterPos];

            if (addressStr == "*") {
                Addresses   = new DCCAddress[0];
                IsBroadcast = true;
            } else {
                var address = addressStr[0] switch {
                    'L' => new DCCAddress(int.Parse(addressStr[1..])),
                    'S' => new DCCAddress(int.Parse(addressStr[1..]), DCCAddressType.Short),
                    _   => throw new Exception("Invalid Address Provided.")
                };

                Addresses   = [address];
                IsBroadcast = false;
            }

            Action     = (ThrottleActionEnum)commandStr[delimiterPos + Delimiter.Length];
            ActionData = commandStr.Length > delimiterPos + Delimiter.Length + 1 ? commandStr[(delimiterPos + Delimiter.Length + 1)..] : "";
            IsValid    = true;
        } catch {
            IsValid = false;
        }
    }

    public string               Message     { get; private set; }
    public char                 Group       { get; private set; }
    public ThrottleFunctionEnum Function    { get; private set; }
    public ThrottleActionEnum   Action      { get; private set; }
    public string               ActionData  { get; private set; }
    public DCCAddress[]         Addresses   { get; private set; }
    public bool                 IsBroadcast { get; private set; }
    public bool                 IsValid     { get; private set; }
}