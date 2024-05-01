using DCCRailway.Application.WiThrottle.Commands;
using DCCRailway.Application.WiThrottle.Helpers;
using DCCRailway.Common.Helpers;

namespace DCCRailway.Application.WiThrottle;

public class WiThrottleCmdFactory() {

    /// <summary>
    ///     Simply, given an input string, this will return a Command Object that
    ///     needs to be managed and processed based on the commandStr provided.
    /// </summary>
    /// <param name="commandStr">A string of data, in raw BYTE form that has been received</param>
    /// <returns></returns>
    public static bool Interpret(WiThrottleConnection connection, WiThrottleServerOptions options, string commandStr) {

        // When we get here, there will only ever be a single message as
        // we filter out in the server when we read each line.
        // ------------------------------------------------------------------
        if (!string.IsNullOrEmpty(commandStr) && commandStr.Length >= 1) {
            var cmdProcessor = DetermineCommandType(commandStr);
            Logger.Log.Debug("CmdFactory [{0}]: Recieved a command of {1}", connection.ConnectionID, cmdProcessor.ToString());
            cmdProcessor.Execute(commandStr);
            if (cmdProcessor is CmdQuit) return true;
        }
        return false;


        IThrottleCmd DetermineCommandType(string commandStr) {
            if (string.IsNullOrEmpty(commandStr)) return new CmdIgnore(connection, options);
            var commandChar = (int)commandStr[0];
            var commandType = Enum.IsDefined(typeof(CommandType), commandChar) ? (CommandType)commandChar : CommandType.Ignore;
            return commandType switch {
                CommandType.Name          => new CmdName(connection, options),
                CommandType.Hardware      => new CmdHardware(connection, options),
                CommandType.MultiThrottle => new CmdMultiThrottle(connection, options),
                CommandType.Panel         => new CmdPanel(connection, options),
                CommandType.Roster        => new CmdRoster(connection, options),
                CommandType.Heartbeat     => new CmdHeartbeat(connection, options),
                CommandType.Quit          => new CmdQuit(connection, options),
                _                         => new CmdIgnore(connection, options)
            };
        }
    }
}

public enum CommandType {
    Name          = 'N',
    Hardware      = 'H',
    MultiThrottle = 'M',
    Panel         = 'P',
    Roster        = 'R',
    Heartbeat     = '*',
    Quit          = 'Q',
    Ignore        = 'X'
}