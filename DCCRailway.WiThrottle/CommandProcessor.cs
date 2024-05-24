﻿using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.Controllers;
using DCCRailway.WiThrottle.Commands;
using Serilog;

namespace DCCRailway.WiThrottle;

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
            var commandType = Enum.IsDefined(typeof(CommandType), commandChar) ? (CommandType)commandChar : CommandType.Ignore;

            return commandType switch {
                CommandType.Name          => new CmdName(logger, connection),
                CommandType.Hardware      => new CmdHardware(logger, connection),
                CommandType.MultiThrottle => new CmdMultiThrottle(logger, connection),
                CommandType.Panel         => new CmdPanel(logger, connection),
                CommandType.Roster        => new CmdRoster(logger, connection),
                CommandType.Heartbeat     => new CmdHeartbeat(logger, connection),
                CommandType.Quit          => new CmdQuit(logger, connection),
                _                         => new CmdIgnore(logger, connection)
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