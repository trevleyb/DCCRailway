using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DCCRailway.Layout.Layout.Base;
using DCCRailway.Layout.Layout.Collection;
using DCCRailway.Layout.Layout.Events;
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
        Assert.That(action,Is.EqualTo(RepositoryChangeAction.Add));
        Assert.That(repository,Is.EqualTo("TestEntities"));
        Assert.That(id,Is.EqualTo(addEntity.Id));

        var entity = collection.IndexOf(0);
        Assert.That(entity,Is.Not.Null);
        entity.Name = "Updated Entity";
        collection.UpdateAsync(entity);
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

public class TestEntities(string prefix = "TEST") : LayoutRepository<TestEntity>(prefix) {

}

public class TestEntity : LayoutEntity {

    private string _id;
    private string _name;
    public new string Id    { get => _id;    set => SetField(ref _id, value); }
    public new string Name  { get => _name;  set => SetField(ref _name, value); }

}