using DCCRailway.Common.Helpers;

namespace DCCRailway.Common.Test.Utility;

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
        Assert.That(propertyChangedEventRaised, Is.True);
        Assert.That(propertyName, Is.EqualTo("Name"));
    }
}