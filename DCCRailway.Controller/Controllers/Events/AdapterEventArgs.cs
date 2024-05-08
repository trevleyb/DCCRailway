using DCCRailway.Controller.Adapters.Base;

namespace DCCRailway.Controller.Controllers.Events;

public enum AdapterEventType {Attach, Detatch, DataSent, DataRecv, Error}

public class AdapterEventArgs(IAdapter adapter, AdapterEventType adapterEvent, byte[]? adapterData = null, string? message = "") : ControllerEventArgs(null, adapter, null, null, message) {
    public AdapterEventType AdapterEvent { get; set; } = adapterEvent;
    public byte[]?          Data         { get; set; } = adapterData;
}