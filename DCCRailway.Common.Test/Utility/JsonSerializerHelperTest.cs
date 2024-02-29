using NUnit.Framework;
using System;
using System.IO;
using System.Text.Json;

namespace DCCRailway.Common.Utilities.Tests;

[TestFixture]
public class JsonSerializerHelperTest {
    private const string TestFileName = "test.json";

    [SetUp]
    public void SetUp() {
        // Clean up the test file before each test
        if (File.Exists(TestFileName)) File.Delete(TestFileName);
    }

    [Test]
    public void Load_ShouldDeserializeObject_WhenFileExists() {
        // Arrange
        var expectedObject = new TestObject { Name = "Test" };
        var serializedStr  = JsonSerializer.Serialize(expectedObject);
        File.WriteAllText(TestFileName, serializedStr);

        // Act
        var loadedObject = JsonSerializerHelper<TestObject>.Load(TestFileName);

        // Assert
        Assert.IsNotNull(loadedObject);
        Assert.AreEqual(expectedObject.Name, loadedObject.Name);
    }

    [Test]
    public void Load_ShouldThrowException_WhenFileDoesNotExist() {
        // Arrange
        var nonExistentFileName = "nonexistent.json";

        // Act & Assert
        Assert.Throws<FileNotFoundException>(() => JsonSerializerHelper<TestObject>.Load(nonExistentFileName));
    }

    [Test]
    public void Load_ShouldThrowException_WhenDeserializationFails() {
        // Arrange
        File.WriteAllText(TestFileName, "invalid json");

        // Act & Assert
        Assert.Throws<ApplicationException>(() => JsonSerializerHelper<TestObject>.Load(TestFileName));
    }

    [Test]
    public void Save_ShouldSerializeObjectAndSaveToFile() {
        // Arrange
        var objectToSave = new TestObject { Name = "Test" };

        // Act
        JsonSerializerHelper<TestObject>.Save(objectToSave, TestFileName);

        // Assert
        Assert.IsTrue(File.Exists(TestFileName));
        var serializedStr      = File.ReadAllText(TestFileName);
        var deserializedObject = JsonSerializer.Deserialize<TestObject>(serializedStr);
        Assert.IsNotNull(deserializedObject);
        Assert.AreEqual(objectToSave.Name, deserializedObject.Name);
    }

    [Test]
    public void Save_ShouldThrowException_WhenFileNameIsNullOrEmpty() {
        // Arrange
        var objectToSave = new TestObject { Name = "Test" };

        // Act & Assert
        Assert.Throws<ApplicationException>(() => JsonSerializerHelper<TestObject>.Save(objectToSave, null));
        Assert.Throws<ApplicationException>(() => JsonSerializerHelper<TestObject>.Save(objectToSave, string.Empty));
    }

    private class TestObject {
        public string Name { get; set; }
    }
}