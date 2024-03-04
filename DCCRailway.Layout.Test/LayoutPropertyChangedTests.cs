using System.ComponentModel;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using DCCRailway.Layout.Entities;
using System.Reflection;
using DCCRailway.Common.Types;
using DCCRailway.Layout.Entities.Base;

namespace DCCRailway.Test.SystemTests;

[TestFixture]
public class LayoutPropertyChangedTests {

    public bool   triggeredChanging = false;
    public bool   triggeredChanged  = false;
    public string activeField       = "";


    [Test]
    public void LocoPropertyChangedEvent() {

        var loco = new Locomotive();
        loco.PropertyChanged += LocoOnPropertyChanged;
        loco.PropertyChanging += LocoOnPropertyChanging;

        TestField(loco, nameof(loco.Name), "LOCO1");
        TestField(loco, nameof(loco.Name), "LOCO2");
        TestField(loco, nameof(loco.Description), "A Description");
        TestField(loco, nameof(loco.Description), "Another Description");
        TestField(loco, nameof(loco.Model), "DDX40AC");
        TestField(loco, nameof(loco.Model), "DDX40ABCD");
        TestField(loco, nameof(loco.Direction), DCCDirection.Forward);
        TestField(loco, nameof(loco.Direction), DCCDirection.Reverse);
        TestField(loco, nameof(loco.Speed), new DCCSpeed(12));
        TestField(loco, nameof(loco.Speed), new DCCSpeed(21));
        TestField(loco, nameof(loco.Type), "Big Type");
        TestField(loco, nameof(loco.Type), "Small Type");
        TestField(loco, nameof(loco.Manufacturer), "NCEDCC");
        TestField(loco, nameof(loco.Manufacturer), "ScaleTrains");
        TestField(loco, nameof(loco.Momentum), new DCCMomentum(99));
        TestField(loco, nameof(loco.Momentum), new DCCMomentum(49));
        TestField(loco, nameof(loco.Momentum), new DCCMomentum(0));

        //TestField(loco, nameof(loco.FunctionBlocks),new DCCFunctionBlocks());
        TestField(loco, nameof(loco.RoadName), "Canadaian National");
        TestField(loco, nameof(loco.RoadName), "CSX");
        TestField(loco, nameof(loco.RoadName), "Union Pacific");
        TestField(loco, nameof(loco.RoadNumber), "CN1234");
        TestField(loco, nameof(loco.RoadNumber), "UP9999");

        loco.PropertyChanged  -= LocoOnPropertyChanged;
        loco.PropertyChanging -= LocoOnPropertyChanging;

    }

    private void LocoOnPropertyChanging(object? sender, PropertyChangingEventArgs e) {
        Assert.That(e.PropertyName, Is.EqualTo(activeField));
        triggeredChanging = true;
    }

    private void LocoOnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
        Assert.That(e.PropertyName, Is.EqualTo(activeField));
        triggeredChanged = true;
    }

    void TestField<T>(ConfigBase entity, string field, T value) {
        triggeredChanging = false;
        triggeredChanged = false;
        activeField      = field;
        var oldValue = GetPropertyValue<T>(entity, field);
        SetPropertyValue(entity, field, value);
        var getValue = GetPropertyValue<T>(entity, field);
        Assert.That(getValue, Is.EqualTo(value));
        Assert.That(getValue, Is.Not.EqualTo(oldValue));
        Assert.That(triggeredChanging, Is.True);
        Assert.That(triggeredChanged, Is.True);
        activeField = "";
    }


    static void SetPropertyValue<T>(object obj, string propertyName, T value) {
        var propertyInfo = obj.GetType().GetProperty(propertyName);
        if (propertyInfo != null && propertyInfo.CanWrite) {
            propertyInfo.SetValue(obj, value);
        }
    }

    static T? GetPropertyValue<T>(object obj, string propertyName) {
        var propertyInfo = obj.GetType().GetProperty(propertyName);
        if (propertyInfo != null && propertyInfo.CanRead) {
            return (T?)propertyInfo.GetValue(obj);
        }
        return default(T);
    }
}