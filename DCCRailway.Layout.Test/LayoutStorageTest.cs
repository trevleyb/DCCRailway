using DCCRailway.Common.Helpers;
using DCCRailway.Layout.Base;
using DCCRailway.Layout.Collection;

namespace DCCRailway.Layout.Test;

[TestFixture]
public class LayoutStorageTest {
    [Test]
    public void TestCreatingaRepositoryWhichShouldBeEmpty() {
        var testRepo = new TestRepository();
        Assert.That(testRepo.Count, Is.EqualTo(0), "Should be nothing in the Repository");
    }

    [Test]
    public void TestSavingAndLoadingARepository() {
        var testRepo = new TestRepository();
        Assert.That(testRepo.Count, Is.EqualTo(0), "Should be nothing in the Repository");
        testRepo.Add(new TestRepositoryItem { Id = "001", Name = "Item1", NumberOftheBeast = 666 });
        testRepo.Add(new TestRepositoryItem { Id = "002", Name = "Item2", NumberOftheBeast = 666 });
        testRepo.Add(new TestRepositoryItem { Id = "003", Name = "Item3", NumberOftheBeast = 666 });

        var serStr = LayoutStorage.SerializeLayout<TestRepository, TestRepositoryItem>(testRepo);
        Assert.That(serStr,Is.Not.Null);
        var serObj = LayoutStorage.DeserializeLayout<TestRepository, TestRepositoryItem>(serStr);
        Assert.That(serObj,Is.Not.Null);

    }

    [Serializable]
    public class TestRepository : LayoutRepository<TestRepositoryItem> {
        public TestRepository() : this(null) { }
        public TestRepository(string? prefix= null) {
            Prefix = prefix ?? "L";
        }
    }

    public class TestRepositoryItem : LayoutEntity {
        public int NumberOftheBeast { get; set; }
    }
}