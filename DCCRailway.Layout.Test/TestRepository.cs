using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DCCRailway.Layout.Base;
using DCCRailway.Layout.Collection;
using DCCRailway.Layout.Events;
using Tmds.Linux;

namespace DCCRailway.Layout.Test;

[TestFixture]
public class TestEntityCollectionWithChanges {
    private string repository;
    private string id;
    private RepositoryChangeAction action;

    [Test]
    public void TestCastToEntityChangingProperties() {

        var collection = new TestEntities();
        Assert.That(collection, Is.Not.Null);
        collection.RepositoryChanged += CollectionOnRepositoryChanged;

        // These will not raise individual events for the collection. Only when changed.
        // -----------------------------------------------------------------------------
        var newEntity = new TestEntity { Name = "Entity 1" };
        var addEntity = collection.Add(newEntity);
        Assert.That(addEntity,Is.Not.Null);
        Assert.That(action,Is.EqualTo(RepositoryChangeAction.Add));
        Assert.That(repository,Is.EqualTo("TestEntities"));
        Assert.That(id,Is.EqualTo(addEntity.Id));

        var entity = collection.IndexOf(0);
        Assert.That(entity,Is.Not.Null);
        entity.Name = "Updated Entity";
        collection.Update(entity);
        Assert.That(action,Is.EqualTo(RepositoryChangeAction.Update));
        Assert.That(repository,Is.EqualTo("TestEntities"));
        Assert.That(id,Is.EqualTo(collection.IndexOf(0)?.Id));
    }

    private void CollectionOnRepositoryChanged(object sender, RepositoryChangedEventArgs args) {
        action = args.Action;
        id = args.Id;
        repository = args.Repository;
    }
}

public class TestEntities(string prefix = "TEST") : LayoutRepository<TestEntity>(prefix,"TEST","./test") { }

public class TestEntity : LayoutEntity { }