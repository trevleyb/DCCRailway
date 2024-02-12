using DCCRailway.System.Adapters.Events;
using DCCRailway.System.Commands;
using DCCRailway.System.Utilities;

namespace DCCRailway.System.Adapters;

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
        Console.WriteLine("Sending Data {0} => {1}", data.ToDisplayValues(), command.Info().Name);
    }

    public byte[]? RecvData(ICommand command) {
        Console.WriteLine("Receiving Data <= {0}", command.Info().Name);
        return [];
    }
}