using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DCCRailway.Layout.Configuration.Entities;
using DCCRailway.Layout.Configuration.Entities.Base;
using DCCRailway.Layout.Configuration.Entities.Collection;
using DCCRailway.Layout.Configuration.Entities.Events;

namespace DCCRailway.Layout.Test;

[TestFixture]
public class TestEntityCollection {

    private int propertyChanged = 0;
    private int propertyChanging = 0;

    [TestCase]
    public void TestEntityCollectionBasics() {

        var collection = new TestEntities();
        Assert.That(collection,Is.Not.Null);

        collection.CollectionChanged += CollectionOnCollectionChanged;
        collection.PropertyChanged += CollectionOnPropertyChanged;
        collection.PropertyChanging += CollectionOnPropertyChanging;

        // These will not raise individual events for the collection. Only when changed.
        // -----------------------------------------------------------------------------
        collection.Add(new TestEntity { Name = "Entity 1" });
        collection.Add(new TestEntity { Name = "Entity 2" });
        collection.Add(new TestEntity { Name = "Entity 3" });

        var entity = collection[0];
        entity.Name = "Updated Entity";
        Assert.That(propertyChanging,Is.EqualTo(1));
        Assert.That(propertyChanged,Is.EqualTo(1));
    }

    private void CollectionOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
        // Do nothing but this advised if the collection has changed.
    }

    private void CollectionOnPropertyChanging(object? sender, PropertyChangingEventArgs e) {
        propertyChanging++;
    }

    private void CollectionOnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
        propertyChanged++;
    }
}

[TestFixture]
public class TestEntityCollectionWithChanges {

    object? propertyChangedValue;
    object? propertyChangingValue;


    [TestCase]
    public void TestCastToEntityChangingProperties() {

        var collection = new TestEntities();
        Assert.That(collection, Is.Not.Null);

        collection.CollectionChanged += CollectionOnCollectionChanged;
        collection.PropertyChanged += CollectionOnPropertyChanged;
        collection.PropertyChanging += CollectionOnPropertyChanging;

        // These will not raise individual events for the collection. Only when changed.
        // -----------------------------------------------------------------------------
        var guid = Guid.NewGuid();
        collection.Add(new TestEntity { Id = guid, Name = "Entity 1" });

        var entity = collection[0];
        entity.Name = "Updated Entity";
        Assert.That(propertyChangingValue, Is.EqualTo("Entity 1"));
        Assert.That(propertyChangedValue, Is.EqualTo("Updated Entity"));

        var newGuid = Guid.NewGuid();
        entity.Id = newGuid;
        Assert.That(propertyChangingValue, Is.EqualTo(guid));
        Assert.That(propertyChangedValue, Is.EqualTo(newGuid));

    }

    private void CollectionOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
        // Do nothing but this advised if the collection has changed.
    }

    private void CollectionOnPropertyChanging(object? sender, PropertyChangingEventArgs e) {
        if (e is EntityPropertyChangingEventArgs args) {
            propertyChangingValue = args.Value;
        }
    }

    private void CollectionOnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
        if (e is EntityPropertyChangedEventArgs args) {
            propertyChangedValue = args.Value;
        }
    }
}


public class TestEntities : EntityCollection<TestEntity> { }

public class TestEntity : PropertyChangeBase, IEntity {

    private Guid _id;
    private string _name;
    public Guid Id    { get => _id;    set => SetField(ref _id, value); }
    public string Name  { get => _name;  set => SetField(ref _name, value); }

}