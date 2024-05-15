using System.Diagnostics;
using System.Text.Json;
using DCCRailway.Common.Helpers;

namespace DCCRailway.Common.Test.Utility;

[TestFixture]
public class JsonSerializerHelperTest {
    private const string? TestFileName = "test.json";

    [SetUp]
    public void SetUp() {
        // Clean up the test file before each test
        if (File.Exists(TestFileName)) File.Delete(TestFileName ?? "test.json");
    }

    [Test]
    public void Load_ShouldDeserializeObject_WhenFileExists() {
        // Arrange
        var expectedObject = new TestObject { Name = "Test" };
        var serializedStr  = JsonSerializer.Serialize(expectedObject);
        File.WriteAllText(TestFileName ?? "test.json", serializedStr);

        // Act
        var serializer   = new JsonSerializerTest();
        var loadedObject = serializer.Load(TestFileName);

        // Assert
        Assert.IsNotNull(loadedObject);
        Assert.That(loadedObject?.Name, Is.EqualTo(expectedObject.Name));
    }

    [Test]
    public void LoadReturnNullWhenFileDoesNotExist() {
        // Arrange
        var nonExistentFileName = "nonexistent.json";
        var serializer          = new JsonSerializerTest();
        var loaded              = serializer.Load(nonExistentFileName);
        Assert.That(loaded, Is.EqualTo(null));
    }

    [Test]
    public void Load_ShouldThrowException_WhenDeserializationFails() {
        // Arrange
        Debug.Assert(TestFileName != null, nameof(TestFileName) + " != null");
        File.WriteAllText(TestFileName ?? "test.json", "invalid json");

        // Act & Assert
        var serializer = new JsonSerializerTest();
        Assert.Throws<ApplicationException>(() => serializer.Load(TestFileName));
    }

    [Test]
    public void Save_ShouldSerializeObjectAndSaveToFile() {
        // Arrange
        var objectToSave = new TestObject { Name = "Test" };

        // Act
        var serializer = new JsonSerializerTest();
        serializer.Save(objectToSave, TestFileName);

        // Assert
        Assert.IsTrue(File.Exists(TestFileName));
        var serializedStr      = File.ReadAllText(TestFileName ?? "test.json");
        var deserializedObject = JsonSerializer.Deserialize<TestObject>(serializedStr);
        Assert.IsNotNull(deserializedObject);
        Assert.That(deserializedObject?.Name, Is.EqualTo(objectToSave.Name));
    }

    [Test]
    public void Save_ShouldThrowException_WhenFileNameIsNullOrEmpty() {
        // Arrange
        var objectToSave = new TestObject { Name = "Test" };

        // Act & Assert
        var serializer = new JsonSerializerTest();
        Assert.Throws<ApplicationException>(() => serializer.Save(objectToSave, null));
        Assert.Throws<ApplicationException>(() => serializer.Save(objectToSave, string.Empty));
    }

    private class JsonSerializerTest : JsonSerializerHelper<TestObject> {
        public void        Save(TestObject obj, string? filename = null) => SaveFile(obj, filename);
        public TestObject? Load(string? filename = null)                 => LoadFile(filename);
    }

    private class TestObject {
        public string Name { get; init; }
    }
}