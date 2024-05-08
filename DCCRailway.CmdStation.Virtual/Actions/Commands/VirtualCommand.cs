using System;
using DCCRailway.CmdStation.Actions;
using DCCRailway.CmdStation.Actions.Results;
using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Virtual.Actions.Validators;
using DCCRailway.Common.Helpers;

namespace DCCRailway.CmdStation.Virtual.Actions.Commands;

public abstract class VirtualCommand : Command {
    public override ICmdResult Execute(IAdapter adapter)
        => SendAndReceive(adapter, new VirtualStandardValidation(), ToString()?.ToByteArray() ?? Array.Empty<byte>());
}