using DCCRailway.Application.WiThrottle.Messages;
using DCCRailway.Common.Helpers;
using DCCRailway.Common.Types;
using DCCRailway.Layout.Conversion.JMRI;
using DCCRailway.Station.Commands.Types;
using Microsoft.VisualBasic;

namespace DCCRailway.Application.WiThrottle.Commands;

public class CmdMultiThrottle (WiThrottleConnection connection, WiThrottleServerOptions options) : ThrottleCmd, IThrottleCmd {
    public void Execute(string commandStr) {
        Logger.Log.Information("{0}=>'{1}'",ToString(),commandStr);
        try {
            IThrottleMsg? response = null;
            var data = new MultiThrottleData(commandStr);
            if (!data.IsValid) return;

            // Process the data based on the Command Function (first 3 characters)
            // ------------------------------------------------------------------------------------------------------
            Logger.Log.Information("{0}=>'{1}' Split into: '{2}'.'{3}' => '{4}'", ToString(), commandStr, data.Function, data.Address, data.Action);

            response = data.Function switch {
                '+' => RequestLocoAccess(data),
                '-' => ReleaseLocoAccess(data),
                'S' => StealLocoAddress(data),
                'L' => ProvideLocoFunctions(data),
                'A' => PerformLocoAction(data),
                _   => null
            };
            if (response is not null) connection.ServerMessages.Add(response);
        }
        catch {
            Logger.Log.Error("{0}: Unable to Process the command =>'{1}'", ToString(), commandStr);
        }
    }

    private IThrottleMsg? RequestLocoAccess(MultiThrottleData data) {
        throw new NotImplementedException();
    }

    private IThrottleMsg? ReleaseLocoAccess(MultiThrottleData data) {
        throw new NotImplementedException();
    }

    private IThrottleMsg? StealLocoAddress(MultiThrottleData data) {
        throw new NotImplementedException();
    }

    private IThrottleMsg? PerformLocoAction(MultiThrottleData data) {
        throw new NotImplementedException();
    }

    private IThrottleMsg? ProvideLocoFunctions(MultiThrottleData data) {
        throw new NotImplementedException();
    }

    public override string ToString() => "CMD:MultiThrottle";

    /// <summary>
    /// Class that breaks up a command string into the 3 consituent parts ensuring that the data is also valid.
    /// </summary>
    internal sealed class MultiThrottleData {
        public char         Group    { get; }
        public char         Function { get; }
        public DCCAddress   Address  { get; }
        public string       Action   { get; }
        public bool         IsValid  { get; }

        private static readonly string Delimiter = "<;>";
        private static readonly char[] ValidFunctions = ['+', '-', 'S', 'A', 'L'];

        public MultiThrottleData(string commandStr) {
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
                var address = commandStr[2..delimiterPos];
                if (address == "*") {
                    Address = new DCCAddress(0, DCCAddressType.Broadcast);
                }
                else {
                    Address = address[0] switch {
                        'L' => new DCCAddress(int.Parse(address[1..]), DCCAddressType.Long),
                        'S' => new DCCAddress(int.Parse(address[1..]), DCCAddressType.Short),
                        _   => throw new Exception("Invalid Address Provided.")
                    };
                }
                Action = commandStr[(delimiterPos+Delimiter.Length)..];
                IsValid = true;
            }
            catch (Exception ex) {
                Logger.Log.Error("{0}: Unknown MultiThrottle Command=>'{1}'",ToString(),commandStr);
                IsValid = false;
            }
        }
    }
}