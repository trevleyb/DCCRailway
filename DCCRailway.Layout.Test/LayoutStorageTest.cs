using DCCRailway.Layout.Base;
using DCCRailway.Layout.Collection;
using DCCRailway.Layout.Entities;

namespace DCCRailway.Layout.Test;

[TestFixture]
public class LayoutStorageTest {

    [Test]
    public void TestCreatingaRepositoryWhichShouldBeEmpty() {
        var TestRepo = new TestRepository();
        Assert.That(TestRepo.FileName,Is.EqualTo("DCCRailway.TestRepository.json"));
        Assert.That(TestRepo.Count,Is.EqualTo(0),"Should be nothing in the Repository");
    }

    [Test]
    public void TestSavingAndLoadingARepository() {
        var TestRepo = new TestRepository();
        Assert.That(TestRepo.FileName,Is.EqualTo("DCCRailway.TestRepository.json"));
        Assert.That(TestRepo.Count,Is.EqualTo(0),"Should be nothing in the Repository");
        TestRepo.Add(new TestRepositoryItem() { Id = "001", Name = "Item1", NumberOftheBeast = 666 });
        TestRepo.Add(new TestRepositoryItem() { Id = "002", Name = "Item2", NumberOftheBeast = 666 });
        TestRepo.Add(new TestRepositoryItem() { Id = "003", Name = "Item3", NumberOftheBeast = 666 });
        TestRepo.FileName = "TestSavingAndLoadingARepository.json";
        TestRepo.Save();
        TestRepo.Load();
        Assert.That(TestRepo.FileName,Is.EqualTo("TestSavingAndLoadingARepository.json"));
        Assert.That(TestRepo.Count,Is.EqualTo(3),"Should have 3 items in our repository");
    }

    public class TestRepository() : LayoutRepository<TestRepositoryItem>("XOX")  { }

    public class TestRepositoryItem : LayoutEntity {
        public int NumberOftheBeast { get; set; }
        public Parameters Parameters { get; set; } = [];
    }
}