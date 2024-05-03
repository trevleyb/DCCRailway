using DCCRailway.Application.WiThrottle.Commands;
using DCCRailway.Application.WiThrottle.Helpers;
using DCCRailway.Common.Helpers;

namespace DCCRailway.Application.WiThrottle;

public class WiThrottleCmdProcessor() {
    /// <summary>
    ///     Simply, given an input string, this will return a Command Object that
    ///     needs to be managed and processed based on the commandStr provided.
    /// </summary>
    /// <param name="connection">A reference to the connection that this command is for</param>
    /// <param name="commandStr">A string of data, in raw BYTE form that has been received</param>
    /// <returns></returns>
    public static bool Interpret(WiThrottleConnection connection, string commandStr) {

        connection.UpdateHeartbeat();

        // When we get here, there will only ever be a single message as
        // we filter out in the server when we read each line.
        // ------------------------------------------------------------------
        if (!string.IsNullOrEmpty(commandStr) && commandStr.Length >= 1) {
            var cmdProcessor = DetermineCommandType();
            Logger.Log.Debug("CMD:Processor [{0}]: Recieved a command of {1}", connection.ToString(), cmdProcessor.ToString());
            cmdProcessor.Execute(commandStr);
            if (cmdProcessor is CmdQuit) return true;
        }
        return false;


        IThrottleCmd DetermineCommandType() {
            if (string.IsNullOrEmpty(commandStr)) return new CmdIgnore(connection);
            var commandChar = (int)commandStr[0];
            var commandType = Enum.IsDefined(typeof(CommandType), commandChar) ? (CommandType)commandChar : CommandType.Ignore;
            return commandType switch {
                CommandType.Name          => new CmdName(connection),
                CommandType.Hardware      => new CmdHardware(connection),
                CommandType.MultiThrottle => new CmdMultiThrottle(connection),
                CommandType.Panel         => new CmdPanel(connection),
                CommandType.Roster        => new CmdRoster(connection),
                CommandType.Heartbeat     => new CmdHeartbeat(connection),
                CommandType.Quit          => new CmdQuit(connection),
                _                         => new CmdIgnore(connection)
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