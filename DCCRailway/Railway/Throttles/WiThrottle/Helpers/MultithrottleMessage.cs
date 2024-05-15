using DCCRailway.Common.Helpers;
using DCCRailway.Common.Types;

namespace DCCRailway.Railway.Throttles.WiThrottle.Helpers;

/// <summary>
///     Class that breaks up a command string into the 3 consituent parts ensuring that the data is also valid.
/// </summary>
public class MultiThrottleMessage {
    private static readonly string Delimiter      = "<;>";
    private static readonly char[] ValidFunctions = ['+', '-', 'S', 'A', 'L'];

    public MultiThrottleMessage(string commandStr) {
        //
        // The command is always made up in the following format(s)
        // 1. [3 char command][address]<;>[action]
        // 2. 3 charcter command is always M[G][Function]
        // 3. G is the Group so we can group locos
        // 4. Function must be one of + - S A L
        //
        try {
            if (commandStr[0] != 'M') throw new Exception("Invalid Multi-Throttle Command.");
            Group    = commandStr[1];
            Function = commandStr[2];
            if (!ValidFunctions.Contains(Function)) throw new Exception("Function provided is not Valid");
            var delimiterPos = commandStr.IndexOf(Delimiter, StringComparison.Ordinal);
            if (delimiterPos == -1 || delimiterPos == 0 || delimiterPos + 1 > commandStr.Length) throw new Exception("Invalid Multi-Throttle Message");

            // Check the address. Can either be * (all in the group) or L or S followed by the DCC Address
            var address = commandStr[3..delimiterPos];
            if (address == "*")
                Address = new DCCAddress(0, DCCAddressType.Broadcast);
            else
                Address = address[0] switch {
                    'L' => new DCCAddress(int.Parse(address[1..])),
                    'S' => new DCCAddress(int.Parse(address[1..]), DCCAddressType.Short),
                    _   => throw new Exception("Invalid Address Provided.")
                };
            Action  = commandStr[(delimiterPos + Delimiter.Length)..];
            IsValid = true;
        } catch {
            Logger.Log.Error("CMD:MultiThrottleMessage: Unknown MultiThrottle Command=>'{0}'", commandStr);
            IsValid = false;
        }
    }

    public char       Group    { get; }
    public char       Function { get; }
    public DCCAddress Address  { get; }
    public string     Action   { get; }
    public bool       IsValid  { get; }
}