using System;
using DCCRailway.Common.Helpers;
using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Virtual.Actions.Validators;

namespace DCCRailway.Controller.Virtual.Actions.Commands;

public abstract class VirtualCommand : Command {
    protected override ICmdResult Execute(IAdapter adapter)
        => SendAndReceive(adapter, new VirtualStandardValidation(), ToString()?.ToByteArray() ?? Array.Empty<byte>());
}