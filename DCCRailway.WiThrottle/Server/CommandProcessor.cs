using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.Controllers;
using DCCRailway.WiThrottle.Server.Commands;
using Serilog;
using CmdName = DCCRailway.WiThrottle.Server.Commands.CmdName;
using CmdPanel = DCCRailway.WiThrottle.Server.Commands.CmdPanel;
using CmdQuit = DCCRailway.WiThrottle.Server.Commands.CmdQuit;
using CmdRoster = DCCRailway.WiThrottle.Server.Commands.CmdRoster;
using IThrottleCmd = DCCRailway.WiThrottle.Server.Commands.IThrottleCmd;

namespace DCCRailway.WiThrottle.Server;

public class CommandProcessor(ILogger logger, ICommandStation commandStation) {
    /// <summary>
    ///     Simply, given an input string, this will return a Command Object that
    ///     needs to be managed and processed based on the commandStr provided.
    /// </summary>
    /// <param name="connection">A reference to the connection that this command is for</param>
    /// <param name="commandStr">A string of data, in raw BYTE form that has been received</param>
    /// <returns></returns>
    public bool Interpret(Connection connection, string commandStr) {
        connection.UpdateHeartbeat();

        // When we get here, there will only ever be a single message as
        // we filter out in the server when we read each line.
        // ------------------------------------------------------------------
        if (!string.IsNullOrEmpty(commandStr) && commandStr.Length >= 1) {
            var cmdProcessor = DetermineCommandType();
            logger.Debug("WiThrottle Command Processor [{0}]: Recieved a command of {1} for {2}", connection.ToString(), cmdProcessor.ToString(), commandStation.AttributeInfo().Name);
            cmdProcessor.Execute(commandStr);
            if (cmdProcessor is CmdQuit) return true;
        }

        return false;

        IThrottleCmd DetermineCommandType() {
            if (string.IsNullOrEmpty(commandStr)) return new CmdIgnore(logger, connection);
            var commandChar = (int)commandStr[0];
            var commandType = Enum.IsDefined(typeof(CommandTypeEnum), commandChar) ? (CommandTypeEnum)commandChar : CommandTypeEnum.Ignore;

            return commandType switch {
                CommandTypeEnum.Name          => new CmdName(logger, connection),
                CommandTypeEnum.Hardware      => new CmdHardware(logger, connection),
                CommandTypeEnum.MultiThrottle => new CmdMultiThrottle(logger, connection),
                CommandTypeEnum.Panel         => new CmdPanel(logger, connection),
                CommandTypeEnum.Roster        => new CmdRoster(logger, connection),
                CommandTypeEnum.Heartbeat     => new CmdHeartbeat(logger, connection),
                CommandTypeEnum.Quit          => new CmdQuit(logger, connection),
                _                             => new CmdIgnore(logger, connection)
            };
        }
    }
}