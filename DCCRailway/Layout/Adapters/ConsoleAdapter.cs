using DCCRailway.Layout.Commands;
using DCCRailway.Utilities;

namespace DCCRailway.Layout.Adapters;

[Adapter("Console", AdapterType.Virtual, "Adapter that writes to the Console", "1.0")]
public class ConsoleAdapter : Adapter, IAdapter {
    
    private bool _connected = false;
    public  bool IsConnected => _connected;
    
    public void Connect() {
        Console.WriteLine("Connected to console.");
        _connected = true;
    }

    public void Disconnect() {
        Console.WriteLine("Disconnected from console.");
        _connected = false;
    }

    public void SendData(byte[] data, ICommand command) {
        Console.WriteLine("Sending Data {0} => {1}", data.ToDisplayValues(), command.AttributeInfo().Name);
    }

    public byte[]? RecvData(ICommand command) {
        Console.WriteLine("Receiving Data <= {0}", command.AttributeInfo().Name);
        return [];
    }
}