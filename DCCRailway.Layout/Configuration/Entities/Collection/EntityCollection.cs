using System.Collections.ObjectModel;
using System.ComponentModel;
using DCCRailway.Common.Helpers;

namespace DCCRailway.Layout.Configuration.Entities.Collection;

[Serializable]
public class EntityCollection<TEntity> : ObservableCollection<TEntity>, IEntityCollection<TEntity> where TEntity : IEntity {

    public string Prefix { get; init; }
    public new event PropertyChangedEventHandler?  PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    public EntityCollection(string prefix) {
        Prefix = prefix;
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

    public string NextID {
        get {
            // sort the current collection and find the highest number in the collection and
            // calculate a new ID based on the entities.Prefix and the next sequential number.
            if (!this.Any()) return $"{Prefix}001";

            try {
                var maxId = this
                           .Where(e => int.TryParse(e.Id.Replace(Prefix, ""), out _))
                           .Max(e => int.Parse(e.Id.Replace(Prefix, "")));

                var nextId = maxId + 1;
                return $"{Prefix}{nextId:D3}";
            }
            catch (Exception ex) {
                Logger.Log.Error("Unable to determine the next sequence: {0}", ex.Message);
                throw;
            }
        }
    }

    public IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken()) {
        //return this.Select(x => Task.FromResult(x)).ToAsyncEnumerable<TEntity>().GetAsyncEnumerator(cancellationToken);
        return this.ToAsyncEnumerable<TEntity>().GetAsyncEnumerator(cancellationToken);
    }
}