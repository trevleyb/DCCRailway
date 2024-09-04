using System.Collections.Specialized;
using DCCRailway.Common.Entities.Base;
using DCCRailway.Common.Entities.Collection;

namespace DCCRailway.Common.Test;

[TestFixture]
public class TestEntityCollectionWithChanges {
    private NotifyCollectionChangedAction _action;

    [Test]
    public void TestCastToEntityChangingProperties() {
        var collection = new TestEntities();
        Assert.That(collection, Is.Not.Null);
        collection.CollectionChanged += CollectionOnCollectionChanged;

        // These will not raise individual events for the collection. Only when changed.
        // -----------------------------------------------------------------------------
        var newEntity = new TestEntity { Name = "Entity 1" };
        var addEntity = collection.Add(newEntity);
        Assert.That(addEntity, Is.Not.Null);
        Assert.That(_action, Is.EqualTo(NotifyCollectionChangedAction.Add));

        var entity = collection[0];
        Assert.That(entity?.Name, Is.Not.Null);
        entity.Name = "Updated Entity";
        collection.Update(entity);
        Assert.That(_action, Is.EqualTo(NotifyCollectionChangedAction.Replace));
    }

    private void CollectionOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
        _action = e.Action;
    }
}

public class TestEntities() : LayoutRepository<TestEntity> { }

public class TestEntity : LayoutEntity { }