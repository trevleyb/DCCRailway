using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DCCRailway.Layout.Configuration.Entities.Base;

public abstract class PropertyChangeBase : INotifyPropertyChanged, INotifyPropertyChanging {

    /// <summary>
    /// Represents a base class for configuration objects that implements the INotifyPropertyChanged and INotifyPropertyChanging interfaces.
    /// </summary>
    public event PropertyChangedEventHandler?  PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null, object? value = null) {
        PropertyChanged?.Invoke(this, new EntityPropertyChangedEventArgs(propertyName,value));
    }
    protected void OnPropertyChanging([CallerMemberName] string? propertyName = null, object? value = null) {
        PropertyChanging?.Invoke(this, new EntityPropertyChangingEventArgs(propertyName,value));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null) {
        if (ReferenceEquals(field, null)) {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        }
        OnPropertyChanging(propertyName,field);
        field = value;
        OnPropertyChanged(propertyName,value);
        return true;
    }

}