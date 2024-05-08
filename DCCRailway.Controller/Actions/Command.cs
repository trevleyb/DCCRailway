using DCCRailway.Common.Helpers;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Actions.Validators;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Exceptions;
using DCCRailway.Controller.Helpers;
using DCCRailway.Controller.Attributes;

namespace DCCRailway.Controller.Actions;

public abstract class Command : PropertyChangedBase, ICommand, IParameterMappable {
    public string Name        => this.AttributeInfo().Name ?? "Unknown";
    public string Version     => this.AttributeInfo().Version ?? "Unknown";
    public string Description => this.AttributeInfo().Description ?? "Unknown";

    public IAdapter? Adapter { get; set; }

    public ICmdResult Execute() => Adapter != null ? Execute(Adapter) : throw new UnsupportedCommandException("No adapter is configured on this command.");
    public abstract ICmdResult Execute(IAdapter adapter);
    public async Task<ICmdResult> ExecuteAsync() => Adapter != null ? await Task.FromResult(Execute(Adapter)) : throw new UnsupportedCommandException("No adapter is configured on this command.");
    public async Task<ICmdResult> ExecuteAsync(IAdapter adapter) => await Task.FromResult(Execute(adapter));

    protected ICmdResult SendAndReceive(IAdapter adapter, IResultValidation validator, byte sendData) => SendAndReceive(adapter, validator, new[] { sendData });
    protected ICmdResult SendAndReceive(IAdapter adapter, IResultValidation validator, byte[] sendData) {
        // Send the command provided to the command station
        // -----------------------------------------------------------------------------------------------------------
        if (adapter == null) throw new ArgumentNullException(nameof(adapter), "The adapter cannot be null.");
        adapter.SendData(sendData, this);

        // All commands with NCE have a response so read back some data
        // ------------------------------------------------------------
        var recvData = adapter.RecvData(this);

        if (recvData == null) throw new ExpectedDataException(null, adapter, "Command expected to receive data from the Adapter but received nothing. ");

        // Validate the data. All NCE Actions return a ! for OK or another code for an error, or some data if appropriate
        // ---------------------------------------------------------------------------------------------------------------
        return validator.Validate(recvData);
    }
}