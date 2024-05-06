namespace DCCRailway.CmdStation.Exceptions;

public class SystemInstantiateException : Exception {
    public SystemInstantiateException(string systemName, string? message, Exception? ex = null) : base(systemName + ":" + message, ex) => SystemName = systemName;

    public string? SystemName { get; }
}