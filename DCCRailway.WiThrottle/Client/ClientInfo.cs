namespace DCCRailway.WiThrottle.Client;

public class ClientInfo(string? name, string address, int port) {
    public ClientInfo(string address, int port) : this(null, address, port) { }

    public string? Name    { get; set; } = name ?? Environment.MachineName ?? "Unknown";
    public string  Address { get; set; } = address;
    public int     Port    { get; set; } = port;
    public Guid    Id      { get; set; } = Guid.NewGuid();

    public string GetNameMessage     => $"N{Name}";
    public string GetHardwareMessage => $"HU{Id.ToString()}";
}