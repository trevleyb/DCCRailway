using System.Diagnostics;
using System.Text.Json;
using DCCRailway.Common.Helpers;

namespace DCCRailway.Common.Test.Utility;

[TestFixture]
public class JsonSerializerHelperTest {
    [SetUp]
    public void SetUp() {
        // Clean up the test file before each test
        if (File.Exists(TestFileName)) File.Delete(TestFileName ?? "test.json");
    }

    private const string? TestFileName = "test.json";

    [Test]
    public void Load_ShouldDeserializeObject_WhenFileExists() {
        // Arrange
        var expectedObject = new TestObject { Name = "Test" };
        var serializedStr  = JsonSerializer.Serialize(expectedObject);
        File.WriteAllText(TestFileName ?? "test.json", serializedStr);

        // Act
        var loadedObject = JsonSerializerHelper<TestObject>.LoadFile(TestFileName);

        // Assert
        Assert.IsNotNull(loadedObject);
        Assert.That(loadedObject?.Name, Is.EqualTo(expectedObject.Name));
    }

    [Test]
    public void LoadReturnNullWhenFileDoesNotExist() {
        // Arrange
        var nonExistentFileName = "nonexistent.json";
        var loaded              = JsonSerializerHelper<TestObject>.LoadFile(nonExistentFileName);
        Assert.That(loaded, Is.EqualTo(null));
    }

    [Test]
    public void Load_ShouldThrowException_WhenDeserializationFails() {
        // Arrange
        Debug.Assert(TestFileName != null, nameof(TestFileName) + " != null");
        File.WriteAllText(TestFileName ?? "test.json", "invalid json");

        // Act & Assert
        Assert.Throws<ApplicationException>(() => JsonSerializerHelper<TestObject>.LoadFile(TestFileName));
    }

    [Test]
    public void Save_ShouldSerializeObjectAndSaveToFile() {
        // Arrange
        var objectToSave = new TestObject { Name = "Test" };

        // Act
        JsonSerializerHelper<TestObject>.SaveFile(objectToSave, TestFileName);

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
        Assert.Throws<ApplicationException>(() => JsonSerializerHelper<TestObject>.SaveFile(objectToSave, string.Empty));
    }

    private class TestObject {
        public string Name { get; init; }
    }
}