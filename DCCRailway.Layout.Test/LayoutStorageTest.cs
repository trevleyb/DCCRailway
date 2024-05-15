using DCCRailway.Layout.Base;
using DCCRailway.Layout.Collection;
using DCCRailway.Layout.Entities;

namespace DCCRailway.Layout.Test;

[TestFixture]
public class LayoutStorageTest {
    [Test]
    public void TestCreatingaRepositoryWhichShouldBeEmpty() {
        var testRepo = new TestRepository("xxx", "testxxx", "./test");
        Assert.That(testRepo.FileName, Is.EqualTo("testxxx.Settings.json"));
        Assert.That(testRepo.FullName, Is.EqualTo("./test/testxxx.Settings.json"));
        Assert.That(testRepo.Count, Is.EqualTo(0), "Should be nothing in the Repository");
    }

    [Test]
    public void TestSavingAndLoadingARepository() {
        var testRepo = new TestRepository("xxx", "testxxx", "./test");
        Assert.That(testRepo.Count, Is.EqualTo(0), "Should be nothing in the Repository");
        testRepo.Add(new TestRepositoryItem() { Id = "001", Name = "Item1", NumberOftheBeast = 666 });
        testRepo.Add(new TestRepositoryItem() { Id = "002", Name = "Item2", NumberOftheBeast = 666 });
        testRepo.Add(new TestRepositoryItem() { Id = "003", Name = "Item3", NumberOftheBeast = 666 });
        testRepo.Name = "TestSavingAndLoadingARepository";
        testRepo.Save();
        testRepo.Load();
        Assert.That(testRepo.FileName, Is.EqualTo("TestSavingAndLoadingARepository.Settings.json"));
        Assert.That(testRepo.Count, Is.EqualTo(3), "Should have 3 items in our repository");
    }

    public class TestRepository(string prefix, string name, string path) : LayoutRepository<TestRepositoryItem>(prefix, name, path) { }

    public class TestRepositoryItem : LayoutEntity {
        public int NumberOftheBeast { get; set; }
    }
}