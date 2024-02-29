using System;
using DCCRailway.Common.Utilities;
using DCCRailway.System.Adapters;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.Manufacturer.Virtual.Commands.Validators;

namespace DCCRailway.System.Manufacturer.Virtual.Commands;

public abstract class VirtualCommand : Command {
    public override ICommandResult Execute(IAdapter adapter) => SendAndReceive(adapter, new VirtualStandardValidation(), ToString()?.ToByteArray() ?? Array.Empty<byte>());
}