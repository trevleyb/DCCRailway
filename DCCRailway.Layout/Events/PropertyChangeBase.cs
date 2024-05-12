using System.ComponentModel;
using System.Runtime.CompilerServices;
using DCCRailway.Layout.Base;

namespace DCCRailway.Layout.Events;

public abstract class PropertyChangeBase : INotifyPropertyChanged {

    /// <summary>
    /// Represents a base class for configuration objects that implements the INotifyPropertyChanged and INotifyPropertyChanging interfaces.
    /// </summary>
    public event PropertyChangedEventHandler?  PropertyChanged;

    protected void OnPropertyChanged(LayoutEntity? entity, [CallerMemberName] string? propertyName = null, object? value = null) {
        PropertyChanged?.Invoke(this, new EntityPropertyChangedEventArgs(entity, propertyName, value));
    }
    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null) {
        if (ReferenceEquals(field, null)) {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        }
        field = value;
        OnPropertyChanged(this as LayoutEntity ?? null, propertyName, value);
        return true;
    }
}