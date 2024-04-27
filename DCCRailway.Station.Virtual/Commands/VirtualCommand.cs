using System;
using DCCRailway.Common.Utilities;
using DCCRailway.Station.Adapters.Base;
using DCCRailway.Station.Commands;
using DCCRailway.Station.Commands.Results;
using DCCRailway.Station.Virtual.Commands.Validators;

namespace DCCRailway.Station.Virtual.Commands;

public abstract class VirtualCommand : Command {
    public override ICommandResult Execute(IAdapter adapter)
        => SendAndReceive(adapter, new VirtualStandardValidation(), ToString()?.ToByteArray() ?? Array.Empty<byte>());
}