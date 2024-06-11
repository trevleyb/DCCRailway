using DCCRailway.Common.Helpers;
using DCCRailway.Common.Parameters;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Actions.Validators;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.Controllers;
using DCCRailway.Controller.Exceptions;
using Serilog;

namespace DCCRailway.Controller.Actions;

public abstract class Command : PropertyChangedBase, ICommand, IParameterMappable {
    public string Name        => this.AttributeInfo().Name ?? "Unknown";
    public string Version     => this.AttributeInfo().Version ?? "Unknown";
    public string Description => this.AttributeInfo().Description ?? "Unknown";

    public ICommandStation CommandStation { get; set; }
    public IAdapter?       Adapter        { get; set; }
    public ILogger         Logger         { get; set; }

    public ICmdResult Execute() {
        if (Adapter is null) throw new ControllerException("No adapter is configured on this command.");
        if (CommandStation is null) throw new ControllerException("Fatal error, command station reference is missing.");
        if (!CommandStation.IsCommandSupported(GetType())) throw new ControllerException("Command is not supported.");

        var result = Execute(Adapter);
        if (result is null) throw new ControllerException("Invalid or missing result from the adapter.");

        result.Command = this;
        CommandStation.OnCommandExecute(CommandStation, this, result);
        return result;
    }

    protected abstract ICmdResult Execute(IAdapter adapter);

    protected ICmdResult SendAndReceive(IAdapter adapter, IResultValidation validator, byte sendData) {
        return SendAndReceive(adapter, validator, new[] { sendData });
    }

    protected ICmdResult SendAndReceive(IAdapter adapter, IResultValidation validator, byte[] sendData) {
        // Send the command provided to the command station
        // -----------------------------------------------------------------------------------------------------------
        if (adapter == null) throw new ArgumentNullException(nameof(adapter), "The adapter cannot be null.");
        adapter.SendData(sendData, this);

        // All commands with NCE have a response so read back some data
        // ------------------------------------------------------------
        var recvData = adapter.RecvData(this);

        if (recvData == null) {
            throw new ExpectedDataException(null, adapter, "Command expected to receive data from the Adapter but received nothing. ");
        }

        // Validate the data. All NCE Actions return a ! for OK or another code for an error, or some data if appropriate
        // ---------------------------------------------------------------------------------------------------------------
        return validator.Validate(recvData);
    }
}