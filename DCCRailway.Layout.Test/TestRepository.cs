using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DCCRailway.Layout.Configuration.Entities;
using DCCRailway.Layout.Configuration.Entities.Base;
using DCCRailway.Layout.Configuration.Entities.Collection;
using DCCRailway.Layout.Configuration.Entities.Events;
using Tmds.Linux;

namespace DCCRailway.Layout.Test;

[TestFixture]
public class TestEntityCollectionWithChanges {
    private string repository;
    private string id;
    private RepositoryChangeAction action;

    [TestCase]
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

        var entity = collection.IndexOf(0).Result;
        Assert.That(entity,Is.Not.Null);
        entity.Name = "Updated Entity";
        collection.UpdateAsync(entity);
        Assert.That(action,Is.EqualTo(RepositoryChangeAction.Update));
        Assert.That(repository,Is.EqualTo("TestEntities"));
        Assert.That(id,Is.EqualTo(collection.IndexOf(0).Result?.Id));
    }

    private void CollectionOnRepositoryChanged(object sender, RepositoryChangedEventArgs args) {
        action = args.Action;
        id = args.Id;
        repository = args.Repository;
    }
}

public class TestEntities(string prefix = "TEST") : Repository<TestEntity>(prefix);

public class TestEntity : PropertyChangeBase, IEntity {

    private string _id;
    private string _name;
    public  string Id    { get => _id;    set => SetField(ref _id, value); }
    public  string Name  { get => _name;  set => SetField(ref _name, value); }

}