using DCCRailway.Layout.Adapters;
using DCCRailway.Layout.Commands.Results;
using DCCRailway.Layout.Commands.Validators;
using DCCRailway.Utilities;
using DCCRailway.Utilities.Exceptions;

namespace DCCRailway.Layout.Commands;

public abstract class Command : PropertyChangedBase, ICommand {
    
    public  string Name        => this.AttributeInfo().Name        ?? "Unknown";
    public  string Version     => this.AttributeInfo().Version     ?? "Unknown";
    public  string Description => this.AttributeInfo().Description ?? "Unknown";
    
    public abstract ICommandResult Execute(IAdapter adapter);
    
    public async Task<ICommandResult> ExecuteAsync(IAdapter adapter) => await Task.FromResult(Execute(adapter));
    
    protected ICommandResult SendAndReceive(IAdapter adapter, IResultValidation validator, byte sendData) => SendAndReceive(adapter, validator, new[] { sendData });
    protected ICommandResult SendAndReceive(IAdapter adapter, IResultValidation validator, byte[] sendData) {

        // Send the command provided to the command station
        // -----------------------------------------------------------------------------------------------------------
        if (adapter == null) throw new ArgumentNullException(nameof(adapter), "The adapter cannot be null.");
        adapter.SendData(sendData, this);

        // All commands with NCE have a response so read back some data
        // ------------------------------------------------------------
        var recvData = adapter.RecvData(this);

        if (recvData == null) throw new ExpectedDataException(null, adapter, "Command expected to receive data from the Adapter but received nothing. ");

        // Validate the data. All NCE Commands return a ! for OK or another code for an error, or some data if appropriate
        // ---------------------------------------------------------------------------------------------------------------
        return validator.Validate(recvData);
    }
}