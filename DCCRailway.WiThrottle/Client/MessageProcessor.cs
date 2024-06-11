using DCCRailway.WiThrottle.Client.Messages;
using DCCRailway.WiThrottle.Server.Commands;
using Serilog;

namespace DCCRailway.WiThrottle.Client;

public class MessageProcessor(ILogger logger) {
    /// <summary>
    ///     Simply, given an input string, this will return a Command Object that
    ///     needs to be managed and processed based on the commandStr provided.
    /// </summary>
    /// <param name="connection">A reference to the connection that this command is for</param>
    /// <param name="commandStr">A string of data, in raw BYTE form that has been received</param>
    /// <returns></returns>
    public IClientMsg Interpret(string commandStr) {
        // When we get here, there will only ever be a single message as
        // we filter out in the server when we read each line.
        // ------------------------------------------------------------------
        if (!string.IsNullOrEmpty(commandStr) && commandStr.Length >= 1) {
            var clientMsg = DetermineClientMsgType();
            logger.Debug("WiThrottle Client Processor: Recieved a message of {0}", clientMsg.ToString());
            clientMsg.Process(commandStr);
            return clientMsg;
        }

        IClientMsg DetermineClientMsgType() {
            if (string.IsNullOrEmpty(commandStr)) return new MsgIgnore(logger);
            var commandChar = (int)commandStr[0];
            var commandType = Enum.IsDefined(typeof(CommandTypeEnum), commandChar) ? (CommandTypeEnum)commandChar : CommandTypeEnum.Ignore;

            return commandType switch {
                CommandTypeEnum.Name          => new MsgName(logger),
                CommandTypeEnum.Hardware      => new MsgHardware(logger),
                CommandTypeEnum.MultiThrottle => new MsgMultiThrottle(logger),
                CommandTypeEnum.Panel         => new MsgPanel(logger),
                CommandTypeEnum.Roster        => new MsgRoster(logger),
                CommandTypeEnum.Heartbeat     => new MsgHeartbeat(logger),
                CommandTypeEnum.Quit          => new MsgQuit(logger),
                _                             => new MsgIgnore(logger)
            };
        }

        return new MsgIgnore(logger);
    }
}