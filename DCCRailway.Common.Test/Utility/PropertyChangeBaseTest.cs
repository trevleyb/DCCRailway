using NUnit.Framework;
using System.ComponentModel;

namespace DCCRailway.Common.Utilities.Tests;

[TestFixture]
public class PropertyChangeBaseTest {
    private class TestClass : PropertyChangedBase {
        private string _name;

        public string Name {
            get => _name;
            set => SetPropertyField(ref _name, value);
        }
    }

    [Test]
    public void SetPropertyField_ShouldRaisePropertyChangedEvent_WhenValueChanges() {
        // Arrange
        var testClass                  = new TestClass();
        var propertyChangedEventRaised = false;
        var propertyName               = "";

        testClass.PropertyChanged += (sender, e) => {
            propertyChangedEventRaised = true;
            propertyName               = e.PropertyName;
        };

        // Act
        testClass.Name = "NewName";

        // Assert
        Assert.IsTrue(propertyChangedEventRaised);
        Assert.AreEqual("Name", propertyName);
    }
}