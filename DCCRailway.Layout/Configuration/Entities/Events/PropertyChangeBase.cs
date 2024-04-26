using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DCCRailway.Layout.Configuration.Entities.Base;

public abstract class PropertyChangeBase : INotifyPropertyChanged, INotifyPropertyChanging {

    /// <summary>
    /// Represents a base class for configuration objects that implements the INotifyPropertyChanged and INotifyPropertyChanging interfaces.
    /// </summary>
    public event PropertyChangedEventHandler?  PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected void OnPropertyChanged(IEntity? entity, [CallerMemberName] string? propertyName = null, object? value = null) {
        PropertyChanged?.Invoke(this, new EntityPropertyChangedEventArgs(entity, propertyName, value));
    }
    protected void OnPropertyChanging(IEntity? entity, [CallerMemberName] string? propertyName = null, object? value = null) {
        PropertyChanging?.Invoke(this, new EntityPropertyChangingEventArgs(entity, propertyName, value));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null) {
        if (ReferenceEquals(field, null)) {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        }

        OnPropertyChanging(this as IEntity ?? null, propertyName, field);
        field = value;
        OnPropertyChanged(this as IEntity ?? null, propertyName, value);
        return true;
    }

}