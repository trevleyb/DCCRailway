using System.Reflection;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Adapters.Events;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.Controllers.Events;
using DCCRailway.Controller.Exceptions;
using Serilog;

namespace DCCRailway.Controller.Controllers;

public class AdapterManager(ILogger logger, ICommandStation commandStation, Assembly assembly) {
    private IAdapter?                          _adapter;
    private Dictionary<Type, AdapterAttribute> _adapters = [];

    public IAdapter? Adapter {
        get => _adapter;
        set {
            if (value != null) {
                if (_adapter != value) Detatch();
                Attach(value);
            }
        }
    }

    public ICommandStation CommandStation => commandStation;

    public List<AdapterAttribute> Adapters {
        get {
            if (_adapters.Any() is false) RegisterAdapters();
            return _adapters.Values.Select(x => x).ToList();
        }
    }

    public event EventHandler<AdapterEventArgs> AdapterEvent;

    public IAdapter? Attach(IAdapter adapter) {
        if (_adapter != null) Detatch();

        OnAdapterAdd(this, adapter);
        _adapter               =  adapter;
        _adapter.DataReceived  += (sender, e) => OnAdapterDataRecv(sender!, _adapter, e);
        _adapter.DataSent      += (sender, e) => OnAdapterDataSent(sender!, _adapter, e);
        _adapter.ErrorOccurred += (sender, e) => OnAdapterError(sender!, _adapter, e);
        return _adapter;
    }

    /// <summary>
    ///     Attach an Adapter by giving the Name of the Adapter to the controller. This will instantiate the
    ///     adapter, and connect it to the physical device.
    /// </summary>
    /// <param name="adapterName">The name of the Adapter to instantiate and connect.</param>
    /// <exception cref="AdapterException">Throws an exception if it cannot find or instantiate the adapter</exception>
    public IAdapter? Attach(string? adapterName) {
        if (Adapters is not { Count: > 0 }) {
            throw new AdapterException(adapterName, "CommandStation has no supported Adapters");
        }

        try {
            foreach (var adapter in _adapters) {
                if (!string.IsNullOrEmpty(adapter.Value.Name) && adapter.Value.Name.Equals(adapterName ?? "", StringComparison.InvariantCultureIgnoreCase)) {
                    var adapterInstance = (IAdapter?)Activator.CreateInstance(adapter.Key, logger);
                    if (adapterInstance != null) return Attach(adapterInstance);
                }

                if (!string.IsNullOrEmpty(adapter.Key.ToString()) && adapter.Key.ToString().Contains(adapterName ?? "", StringComparison.InvariantCultureIgnoreCase)) {
                    var adapterInstance = (IAdapter?)Activator.CreateInstance(adapter.Key, logger);
                    if (adapterInstance != null) return Attach(adapterInstance);
                }

                if (!string.IsNullOrEmpty(adapter.Value.Description) && adapter.Value.Description.Contains(adapterName ?? "", StringComparison.InvariantCultureIgnoreCase)) {
                    var adapterInstance = (IAdapter?)Activator.CreateInstance(adapter.Key, logger);
                    if (adapterInstance != null) return Attach(adapterInstance);
                }
            }
        } catch (Exception ex) {
            throw new AdapterException(adapterName, "Error instantiating the Adapter.", ex);
        }

        throw new AdapterException(adapterName, "CommandStation does not support the specified adapter.");
    }

    private void Detatch() {
        if (_adapter != null) {
            OnAdapterRemoved(this, _adapter);
            _adapter.Disconnect();
            _adapter.Dispose();
            _adapter = null;
        }
    }

    public bool IsAdapterSupported<T>() where T : IAdapter {
        return _adapters.ContainsKey(typeof(T));
    }

    public bool IsAdapterSupported(Type adapter) {
        return _adapters.ContainsKey(adapter);
    }

    public bool IsAdapterSupported(string name) {
        return _adapters.Any(pair => pair.Value.Name != null && pair.Value.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
    }

    private void RegisterAdapters() {
        if (assembly is null) throw new ApplicationException("No Assembly has been set for the Adapter Manager");
        var foundTypes = assembly.DefinedTypes.Where(t => t.ImplementedInterfaces.Contains(typeof(IAdapter)));

        foreach (var adapter in foundTypes) {
            var attr = AttributeExtractor.GetAttribute<AdapterAttribute>(adapter);

            if (attr != null && !string.IsNullOrEmpty(attr.Name)) {
                var adapterInterface = adapter.ImplementedInterfaces.First(x => x.FullName != null && x.FullName.StartsWith("DCCRailway.Controller.Adapters.", StringComparison.InvariantCultureIgnoreCase));
                if (!_adapters.ContainsKey(adapterInterface)) _adapters.TryAdd(adapter, attr);
            }
        }
    }

    protected void RegisterAdapter<T>() where T : IAdapter {
        var attr = AttributeExtractor.GetAttribute<AdapterAttribute>(typeof(T));

        if (attr is null || string.IsNullOrEmpty(attr.Name)) {
            throw new ApplicationException("Adapter instance cannot be NULL and must be a concrete object.");
        }

        if (!_adapters.ContainsKey(typeof(T))) _adapters.TryAdd(typeof(T), attr);
    }

    protected void ClearAdapters() {
        _adapters = [];
    }

    #region Raise Events
    // Raise when we add an Adapter to this controller
    private void OnAdapterAdd(object sender, IAdapter adapter) {
        var e = new AdapterEventArgs(adapter, AdapterEventType.Attach, null, $"Adapter {adapter.GetType().Name} added");
        AdapterEvent(sender, e);
    }

    // Raise when we delete or remove an Adapter from this CommandStation
    private void OnAdapterRemoved(object sender, IAdapter adapter) {
        var e = new AdapterEventArgs(adapter, AdapterEventType.Detatch, null, $"Adapter {adapter.GetType().Name} removed");
        AdapterEvent.Invoke(sender, e);
    }

    private void OnAdapterDataRecv(object sender, IAdapter adapter, DataRecvArgs dataRecvArgs) {
        var e = new AdapterEventArgs(adapter, AdapterEventType.DataRecv, dataRecvArgs.Data, $"Adapter {adapter.GetType().Name} recieved data.");
        AdapterEvent.Invoke(sender, e);
    }

    private void OnAdapterDataSent(object sender, IAdapter adapter, DataSentArgs dataSentArgs) {
        var e = new AdapterEventArgs(adapter, AdapterEventType.DataRecv, dataSentArgs.Data, $"Adapter {adapter.GetType().Name} sent data");
        AdapterEvent.Invoke(sender, e);
    }

    private void OnAdapterError(object sender, IAdapter adapter, DataErrorArgs dataErrorArgs) {
        var e = new AdapterEventArgs(adapter, AdapterEventType.DataRecv, null, $"Adapter {adapter.GetType().Name} recieved error: " + dataErrorArgs.Error);
        AdapterEvent.Invoke(sender, e);
    }
    #endregion
}