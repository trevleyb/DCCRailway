﻿using DCCRailway.Application.WiThrottle.Commands;

namespace DCCRailway.Application.WiThrottle;

public class WiThrottleCmdFactory {
    private          WiThrottleServerOptions   _options;
    private readonly WiThrottleConnectionEntry _connectionEntry;

    public WiThrottleCmdFactory(WiThrottleConnectionEntry connectionEntry, ref WiThrottleServerOptions options) {
        _options = options;
        _connectionEntry = connectionEntry;
    }

    /// <summary>
    ///     Simply, given an input string, this will return a Command Object that
    ///     needs to be managed and processed based on the command provided.
    /// </summary>
    /// <param name="command">A string of data, in raw BYTE form that has been received</param>
    /// <returns></returns>
    public IThrottleCmd Interpret(CommandType type, string command) {
        if (!string.IsNullOrEmpty(command) && command.Length >= 1) {

            // Get rid of the terminator if it is still there
            // --------------------------------------------------------------
            command = command.TrimEnd(_options.Terminator.ToCharArray());

            // Special commands are generated by the SYSTEM
            // --------------------------------------------------------------
            return type switch {
                CommandType.Startup      => new CmdStartup(_connectionEntry, command, ref _options),
                CommandType.ChangeNotify => new CmdStartup(_connectionEntry, command, ref _options),
                CommandType.Alert        => new CmdStartup(_connectionEntry, command, ref _options),
                CommandType.Info         => new CmdStartup(_connectionEntry, command, ref _options),
                CommandType.ServerType   => new CmdStartup(_connectionEntry, command, ref _options),
                CommandType.FastClock    => new CmdStartup(_connectionEntry, command, ref _options),
                CommandType.Client       => ProcessClientCommand(command),
                _                        => new CmdUnknown(_connectionEntry, command, ref _options)
            };
        }
        return new CmdUnknown(_connectionEntry, command, ref _options);
    }

    public IThrottleCmd ProcessClientCommand(string command) {
        var cmdCode = command.Remove(1);
        var cmdData = command.Length > 1 ? command[1..] : "";

        // What is the command? What do we need to do with it?
        // --------------------------------------------------------------
        return cmdCode switch {
            "T" => new CmdThrottle  (_connectionEntry, cmdData, ref _options),
            "S" => new CmdThrottle  (_connectionEntry, cmdData, ref _options),
            "M" => new CmdThrottle  (_connectionEntry, cmdData, ref _options),
            "C" => new CmdThrottle  (_connectionEntry, cmdData, ref _options),
            "D" => new CmdDirect    (_connectionEntry, cmdData, ref _options),
            "*" => new CmdHeartBeat (_connectionEntry, cmdData, ref _options),
            "N" => new CmdDeviceName(_connectionEntry, cmdData, ref _options),
            "H" => new CmdDeviceID  (_connectionEntry, cmdData, ref _options),
            "P" => new CmdPanelCmd  (_connectionEntry, cmdData, ref _options),
            "R" => new CmdRosterCmd (_connectionEntry, cmdData, ref _options),
            "Q" => new CmdQuit      (_connectionEntry, cmdData, ref _options),
            _   => new CmdUnknown   (_connectionEntry, command, ref _options)
        };
    }
}

public enum CommandType {
    Client,
    Startup,
    ChangeNotify,
    FastClock,
    Alert,
    Info,
    ServerType
}