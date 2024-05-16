using DCCRailway.Common.Helpers;
using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Attributes;
using Serilog;

namespace DCCRailway.Controller.Adapters;

[Adapter("Console", AdapterType.Virtual, "Adapter that writes to the Console", "1.0")]
public abstract class ConsoleAdapter(ILogger logger) : Adapter, IAdapter {
    public bool IsConnected { get; private set; }

    public void Connect() {
        Console.WriteLine("Connected to console.");
        IsConnected = true;
    }

    public void Disconnect() {
        Console.WriteLine("Disconnected from console.");
        IsConnected = false;
    }

    public void Dispose() { }

    public void SendData(byte[] data, ICommand command) {
        Console.WriteLine("Sending Data {0} => {1}", data.ToDisplayValues(), command.AttributeInfo().Name);
    }

    public byte[]? RecvData(ICommand command) {
        Console.WriteLine("Receiving Data <= {0}", command.AttributeInfo().Name);

        return [];
    }
}