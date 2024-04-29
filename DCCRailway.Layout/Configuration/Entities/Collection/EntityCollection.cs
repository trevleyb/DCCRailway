using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using DCCRailway.Common.Helpers;

namespace DCCRailway.Layout.Configuration.Entities.Collection;

[Serializable]
public class EntityCollection<TEntity> : ObservableCollection<TEntity>, IEntityCollection<TEntity> where TEntity : IEntity {

    private static readonly SemaphoreSlim _mutex = new SemaphoreSlim(1, 1);
    private string Prefix { get; init; }

    public new event PropertyChangedEventHandler?  PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    public override event NotifyCollectionChangedEventHandler? CollectionChanged {
        add => base.CollectionChanged += value;
        remove => base.CollectionChanged -= value;
    }

    public EntityCollection(string prefix) {
        Prefix = prefix;
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

    /// <summary>
    /// Each item in the collection must be UNIQUE and have a Unique ID. Originally this was a GUID but
    /// that is not user friendly so changed it to be a sequential number with a prefix. This code looks
    /// at the current collection and finds the next available ID.
    /// </summary>
    public async Task<string> GetNextID() {
        await _mutex.WaitAsync();
        try {
            var nextID = 1;

            // sort the current collection and find the highest number in the collection and
            // calculate a new ID based on the entities.Prefix and the next sequential number.
            if (this.Any()) {
                var maxId = this
                           .Where(e => int.TryParse(e.Id.Replace(Prefix, ""), out _))
                           .Max(e => int.Parse(e.Id.Replace(Prefix, "")));
                nextID = maxId + 1;
            }
            return $"{Prefix}{nextID:D3}";
        }
        catch (Exception ex) {
            Logger.Log.Error("Unable to determine the next sequence: {0}", ex.Message);
            throw;
        }
        finally {
            _mutex.Release();
        }
    }

    public IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken()) {
        //return this.Select(x => Task.FromResult(x)).ToAsyncEnumerable<TEntity>().GetAsyncEnumerator(cancellationToken);
        return this.ToAsyncEnumerable<TEntity>().GetAsyncEnumerator(cancellationToken);
    }
}