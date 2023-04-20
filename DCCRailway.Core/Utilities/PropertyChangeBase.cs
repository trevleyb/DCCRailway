using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DCCRailway.Core.Utilities; 

public class PropertyChangedBase : INotifyPropertyChanged {
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) {
        PropertyChanged?.Invoke(this, e);
    }

    protected void SetPropertyField<T>(ref T field, T newValue, [CallerMemberName] string propertyName = "") {
        if (!EqualityComparer<T>.Default.Equals(field, newValue)) {
            field = newValue;
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
    }
}