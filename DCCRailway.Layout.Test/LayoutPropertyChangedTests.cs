using System.ComponentModel;
using System.Runtime.CompilerServices;
using DCCRailway.Layout.Entities;

namespace DCCRailway.Test.SystemTests;

[TestFixture]
public class LayoutPropertyChangedTests {
    [Test]
    public void LocoPropertyChangedEvent() {

        var changingField = "";
        var changedField = "";

        var loco = new Locomotive();
        loco.PropertyChanged += LocoOnPropertyChanged;
        loco.PropertyChanging += LocoOnPropertyChanging;

        // TODO: Need to pass this and test the data and ensure Properties are being raised.
        //var locoName = loco.Name;
        //TestField(ref locoName,"Test Value");

        loco.PropertyChanged  -= LocoOnPropertyChanged;
        loco.PropertyChanging -= LocoOnPropertyChanging;

        void TestField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null) {
            var oldValue = field;
            changedField  = "";
            changingField = "";
            field         = value;
            Assert.That(field,Does.Not.EqualTo(oldValue));
            Assert.That(changedField.Equals(propertyName));
            Assert.That(changingField.Equals(propertyName));
        }

        void LocoOnPropertyChanging(object? sender, PropertyChangingEventArgs e) {
            changingField = e.PropertyName;
        }

        void LocoOnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
            changedField = e.PropertyName;
        }

    }

}