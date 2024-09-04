using System.Collections.Concurrent;
using System.Collections.Specialized;
using System.ComponentModel;

namespace DCCRailway.Common.Entities.Collection;

public class ObservableConcurrentDictionary<TKey, TValue> : INotifyCollectionChanged, INotifyPropertyChanged, IObservableConcurrentDictionary where TKey : notnull {
    private readonly ConcurrentDictionary<TKey, TValue> _dictionary = new ConcurrentDictionary<TKey, TValue>();

    public TValue this[int index] {
        get => _dictionary.Values.ToArray()[index];
        set => AddOrUpdate(_dictionary.Keys.ToArray()[index], value);
    }

    public TValue this[TKey key] {
        get => _dictionary[key];
        set => AddOrUpdate(key, value);
    }

    protected IEnumerable<TKey>   Keys   => _dictionary.Keys;
    public    IEnumerable<TValue> Values => _dictionary.Values;
    public    int                 Count  => _dictionary.Count;

    public IEnumerable<KeyValuePair<TKey, TValue>> AsEnumerable => _dictionary.AsEnumerable();

    public event NotifyCollectionChangedEventHandler? CollectionChanged;
    public event PropertyChangedEventHandler?         PropertyChanged;

    public void Clear() {
        _dictionary.Clear();
    }

    protected bool TryAdd(TKey key, TValue value) {
        if (_dictionary.TryAdd(key, value)) {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, new KeyValuePair<TKey, TValue>(key, value)));
            OnPropertyChanged(nameof(Count));
            return true;
        }

        return false;
    }

    protected bool TryRemove(TKey key, out TValue value) {
        if (_dictionary.TryRemove(key, out value)) {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, new KeyValuePair<TKey, TValue>(key, value)));
            OnPropertyChanged(nameof(Count));
            return true;
        }

        return false;
    }

    protected bool ContainsKey(TKey key) {
        return _dictionary.ContainsKey(key);
    }

    public TValue AddOrUpdate(TKey key, TValue value) {
        var newValue = _dictionary.AddOrUpdate(key, value, (k, v) => value);
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, new KeyValuePair<TKey, TValue>(key, newValue)));
        return newValue;
    }

    protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e) {
        CollectionChanged?.Invoke(this, e);
    }

    protected virtual void OnPropertyChanged(string propertyName) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}