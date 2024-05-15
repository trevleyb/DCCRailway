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
    private string _repository;
    private string _id;
    private RepositoryChangeAction _action;

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
        Assert.That(_action,Is.EqualTo(RepositoryChangeAction.Add));
        Assert.That(_repository,Is.EqualTo("TestEntities"));
        Assert.That(_id,Is.EqualTo(addEntity.Id));

        var entity = collection.IndexOf(0);
        Assert.That(entity,Is.Not.Null);
        entity.Name = "Updated Entity";
        collection.Update(entity);
        Assert.That(_action,Is.EqualTo(RepositoryChangeAction.Update));
        Assert.That(_repository,Is.EqualTo("TestEntities"));
        Assert.That(_id,Is.EqualTo(collection.IndexOf(0)?.Id));
    }

    private void CollectionOnRepositoryChanged(object sender, RepositoryChangedEventArgs args) {
        _action = args.Action;
        _id = args.Id;
        _repository = args.Repository;
    }
}

public class TestEntities(string prefix = "TEST") : LayoutRepository<TestEntity>(prefix,"TEST","./test") { }

public class TestEntity : LayoutEntity { }