using System;
using DCCRailway.DCCController.Adapters;
using DCCRailway.DCCController.Commands;
using DCCRailway.DCCController.Commands.Results;
using DCCRailway.Manufacturer.Virtual.Commands.Validators;
using DCCRailway.Utilities;

namespace DCCRailway.Manufacturer.Virtual.Commands;

public abstract class VirtualCommand : Command {
    public override ICommandResult Execute(IAdapter adapter) {
        return SendAndReceive(adapter, new VirtualStandardValidation(), ToString()?.ToByteArray() ?? Array.Empty<byte>());
    }
}