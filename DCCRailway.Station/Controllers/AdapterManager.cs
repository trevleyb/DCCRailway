using System.Reflection;
using DCCRailway.Station.Adapters.Base;
using DCCRailway.Station.Adapters.Events;
using DCCRailway.Station.Attributes;
using DCCRailway.Station.Controllers.Events;
using DCCRailway.Station.Exceptions;

namespace DCCRailway.Station.Controllers;

public class AdapterManager(Assembly assembly) {

    private IAdapter?                           _adapter;       // Stores the adapter to be used
    private Assembly                            _assembly { get; set; } = assembly;
    private Dictionary<Type, AdapterAttribute>  _adapters = [];
    public event EventHandler<AdapterEventArgs> AdapterEvent;

    public IAdapter? Adapter {
        get => _adapter;
        set {
            if (value != null) {
                if (_adapter != value) Detatch();
                Attach(value);
            }
        }
    }

    protected IAdapter? Attach(IAdapter adapter) {
        if (_adapter != null) Detatch();

        OnAdapterAdd(this, adapter);
        _adapter                =  adapter;
        _adapter.DataReceived  += (sender, e) => OnAdapterDataRecv(sender!, _adapter, e);
        _adapter.DataSent      += (sender, e) => OnAdapterDataSent(sender!, _adapter, e);
        _adapter.ErrorOccurred += (sender, e) => OnAdapterError(sender!, _adapter, e);
        _adapter.Connect();
        return _adapter;
    }

    /// <summary>
    /// Attach an Adapter by giving the Name of the Adapter to the controller. This will instantiate the
    /// adapter, and connect it to the physical device.
    /// </summary>
    /// <param name="adapterName">The name of the Adapter to instantiate and connect.</param>
    /// <exception cref="AdapterException">Throws an exception if it cannot find or instantiate the adapter</exception>
    public IAdapter? Attach(string adapterName) {
        if (Adapters is not { Count: > 0 }) throw new AdapterException(adapterName, "Controller has no supported Adapters");
        try {
            if (_adapters.Any() is false) RegisterAdapters();
            foreach (var adapters in _adapters!) {
                if (adapters.Value.Name.Equals(adapterName, StringComparison.InvariantCultureIgnoreCase)) {
                    var adapterInstance = (IAdapter?)Activator.CreateInstance(adapters.Key);
                    if (adapterInstance != null) return Attach(adapterInstance);
                }
            }
        }
        catch (Exception ex) {
            throw new AdapterException(adapterName, "Error instantiating the Adapter.", ex);
        }
        throw new AdapterException(adapterName, "Controller does not support the specified adapter.");
    }

    private void Detatch() {
        if (_adapter != null) {
            OnAdapterRemoved(this, _adapter);
            _adapter.Disconnect();
            _adapter.Dispose();
            _adapter  = null;
        }
    }

    public bool IsAdapterSupported<T>() where T : IAdapter => _adapters.ContainsKey(typeof(T));
    public bool IsAdapterSupported(Type adapter) => _adapters.ContainsKey(adapter);
    public bool IsAdapterSupported(string name)  => _adapters.Any(pair => pair.Value.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
    public List<AdapterAttribute> Adapters {
        get {
            if (_adapters.Any() is false) RegisterAdapters();
            return _adapters.Values.ToList();
        }
    }

    private void RegisterAdapters() {}

    protected void RegisterAdapter<T>() where T : IAdapter {
        var attr = AttributeExtractor.GetAttribute<AdapterAttribute>(typeof(T));
        if (attr is null || string.IsNullOrEmpty(attr.Name))
            throw new ApplicationException("Adapter instance cannot be NULL and must be a concrete object.");
        if (!_adapters.ContainsKey(typeof(T))) _adapters.TryAdd(typeof(T), attr);
    }

    protected void ClearAdapters() {
        _adapters = [];
    }

    #region Raise Events
    // Raise when we add an Adapter to this controller
    private void OnAdapterAdd(object sender, IAdapter adapter) {
        var e = new AdapterEventArgs(adapter, AdapterEventType.Attach,  null, $"Adapter {adapter.GetType().Name} added");
        AdapterEvent?.Invoke(sender, e);
    }

    // Raise when we delete or remove an Adapter from this Controller
    private void OnAdapterRemoved(object sender, IAdapter adapter) {
        var e = new AdapterEventArgs(adapter, AdapterEventType.Detatch, null, $"Adapter {adapter.GetType().Name} removed");
        AdapterEvent?.Invoke(sender, e);
    }

    private void OnAdapterDataRecv(object sender, IAdapter adapter, DataRecvArgs dataRecvArgs) {
        var e = new AdapterEventArgs(adapter, AdapterEventType.DataRecv, dataRecvArgs.Data, $"Adapter {adapter.GetType().Name} recieved data.");
        AdapterEvent?.Invoke(sender, e);
    }

    private void OnAdapterDataSent(object sender, IAdapter adapter, DataSentArgs dataSentArgs) {
        var e = new AdapterEventArgs(adapter, AdapterEventType.DataRecv, dataSentArgs.Data, $"Adapter {adapter.GetType().Name} sent data");
        AdapterEvent?.Invoke(sender, e);
    }

    private void OnAdapterError(object sender, IAdapter adapter, DataErrorArgs dataErrorArgs) {
        var e = new AdapterEventArgs(adapter, AdapterEventType.DataRecv, null, $"Adapter {adapter.GetType().Name} recieved error: " + dataErrorArgs.Error);
        AdapterEvent?.Invoke(sender, e);
    }
    #endregion

}