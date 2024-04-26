using System.Collections.ObjectModel;
using System.ComponentModel;
using DCCRailway.Layout.Configuration.Entities.Base;

namespace DCCRailway.Layout.Configuration.Entities;

[Serializable]
public class EntityCollection<TKey, TEntity> : ObservableCollection<TEntity>, IEntityCollection<TEntity> where TEntity : IEntity<TKey> where TKey : notnull {

    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    public EntityCollection() {
        Clear();
        CollectionChanged += (sender, e) => {
            if (e.NewItems != null) {
                foreach (TEntity newItem in e.NewItems) {
                    newItem.PropertyChanged  += OnPropertyChanged;
                    newItem.PropertyChanging += OnPropertyChanging;
                }
            }

            if (e.OldItems != null) {
                foreach (TEntity oldItem in e.OldItems) {
                    oldItem.PropertyChanged  -= OnPropertyChanged;
                    oldItem.PropertyChanging -= OnPropertyChanging;
                }
            }
        };
    }

    private void OnPropertyChanging(object? sender, PropertyChangingEventArgs e) {
        PropertyChanging?.Invoke(this, e);
    }

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
        PropertyChanged?.Invoke(this, e);
    }

}