namespace DCCRailway.WiThrottle;

public class ClientInfo {
    public ClientInfo(string address, int port) : this(null, address, port) { }

    public ClientInfo(string? name, string address, int port) {
        Name = name ?? Environment.MachineName ?? "Unknown";
        Port = port;
        Id   = Guid.NewGuid();
    }

    public string? Name    { get; set; }
    public string  Address { get; set; } = string.Empty;
    public int     Port    { get; set; }
    public Guid    Id      { get; set; }

    public string GetNameMessage     => $"N{Name}";
    public string GetHardwareMessage => $"HU{Id.ToString()}";
}