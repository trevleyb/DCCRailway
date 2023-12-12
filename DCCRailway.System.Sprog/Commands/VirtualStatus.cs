﻿using DCCRailway.System.Adapters;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands;
using DCCRailway.System.Commands.CommandType;
using DCCRailway.System.Commands.Result;
using DCCRailway.System.Commands.Validator;
using DCCRailway.System.Utilities;

namespace DCCRailway.System.Virtual.Commands;

[Command("Status", "Status Command")]
public class VirtualStatus : Command, ICmdStatus {
    private readonly byte[] CommandData = "STATUS_COMMAND".ToByteArray();

    public override IResult Execute(IAdapter adapter) {
        var result = SendAndReceive(adapter, new SimpleResultValidation(2), "STATUS_COMMAND".ToByteArray());

        if (!result.OK) return result;

        return new ResultOK(result.Data);
    }
}