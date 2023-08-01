﻿using DCCRailway.System.Adapters;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.Commands.Validators;
using DCCRailway.System.Exceptions;
using DCCRailway.System.Utilities;

namespace DCCRailway.System.Commands;

public abstract class Command : PropertyChangedBase, ICommand {
    public abstract IResult Execute(IAdapter adapter);

    /// <summary>
    ///     Used for Async/Await calls
    /// </summary>
    /// <param name="adapter"></param>
    /// <returns></returns>
    public async Task<IResult> ExecuteAsync(IAdapter adapter) {
        return await Task.FromResult(Execute(adapter));
    }

    protected IResult SendAndReceieve(IAdapter adapter, IResultValidation validator, byte sendData) {
        return SendAndReceieve(adapter, validator, new[] { sendData });
    }

    protected IResult SendAndReceieve(IAdapter adapter, IResultValidation validator, byte[] sendData) {
        // Send the command provided to the command station
        // -----------------------------------------------------------------------------------------------------------
        if (adapter == null) throw new ArgumentNullException(nameof(adapter), "The adapter cannot be null.");
        adapter.SendData(sendData, this);

        // All commands with NCE have a response so read back some data
        // ------------------------------------------------------------
        var recvData = adapter.RecvData(this);

        if (recvData == null) throw new ExpectedDataException(null, adapter, "Command expected to recieve data from the Adapter but recieved nothing. ");

        // Validate the data. All NCE Commands return a ! for OK or another code for an error, or some data if appropriate
        // ---------------------------------------------------------------------------------------------------------------
        return validator.Validate(recvData);
    }
}