using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DCCRailway.Layout.Entities.Base;

public abstract class ConfigBase : INotifyPropertyChanged, INotifyPropertyChanging {
    private string     _name            = "";
    private string     _description     = "";
    private Parameters _parameters      = [];

    public string Name           { get => _name;        set => SetField(ref _name, value); }
    public string Description    { get => _description; set => SetField(ref _description, value); }
    public Parameters Parameters { get => _parameters;  set => SetField(ref _parameters, value); }

    /// <summary>
    /// Represents a base class for configuration objects that implements the INotifyPropertyChanged and INotifyPropertyChanging interfaces.
    /// </summary>
    public event PropertyChangedEventHandler?  PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    protected virtual void OnPropertyChanging([CallerMemberName] string? propertyName = null) {
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null) {
        if (ReferenceEquals(field, null)) {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        }
        OnPropertyChanging(propertyName);
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}