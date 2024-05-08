using System;
using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Commands;
using DCCRailway.CmdStation.Commands.Results;
using DCCRailway.CmdStation.Virtual.Commands.Validators;
using DCCRailway.Common.Helpers;

namespace DCCRailway.CmdStation.Virtual.Commands;

public abstract class VirtualCommand : Command {
    public override ICmdResult Execute(IAdapter adapter)
        => SendAndReceive(adapter, new VirtualStandardValidation(), ToString()?.ToByteArray() ?? Array.Empty<byte>());
}