using System.IO.Ports;
using DCCRailway.Layout.Entities;

namespace DCCRailway.Layout.Test;

[TestFixture]
public class ParameterTests {
    [Test]
    public void TypeConvertTest() {
        var var1                      = 42;
        var typeName                  = var1.GetType().Name;
        var typeFullName              = var1.GetType().FullName;
        var typeAssemblyQualifiedName = var1.GetType().AssemblyQualifiedName;
        var typeToString              = var1.GetType().ToString();

        var convertName                  = Type.GetType(typeName);
        var convertFullName              = Type.GetType(typeFullName ?? typeName);
        var convertAssemblyQualifiedName = Type.GetType(typeAssemblyQualifiedName ?? typeName);
        var convertToString              = Type.GetType(typeToString);
    }

    [Test]
    public void PropertyBagNullExceptionTest() {
        var bag = new Parameters();
        Assert.That(() => bag.Add(null!, null!), Throws.Exception);
        Assert.That(() => bag.Add("Empty", null!), Throws.Exception);
        Assert.That(() => bag.Add(null!, "empty"), Throws.Exception);
        Assert.That(() => bag.Add<string>(null!, null!), Throws.Exception);
        Assert.That(() => bag.Add<string>("Empty", null!), Throws.Exception);
        Assert.That(() => bag.Add<string>(null!, "empty"), Throws.Exception);
        Assert.That(() => bag.Set(null!, "Value"), Throws.Exception);
        Assert.That(() => bag.Set<string>(null!, "Value"), Throws.Exception);
        Assert.That(() => bag.Set("Empty", null!), Throws.Exception);
        Assert.That(() => bag.Set<string>("Empty", null!), Throws.Exception);
        Assert.That(() => bag.Get(null!), Throws.Exception);
        Assert.That(() => bag.Get<string>(null!), Throws.Exception);
    }

    [Test]
    public void PropertyBagSetAndCheckTests() {
        var bag = new Parameters { { "Value1", "Value" } };
        Assert.That(bag.Get("value1")!.Equals("Value"));
        bag.Add("Value2", 42);
        Assert.That(bag.Get("value2")!.Equals(42));
        bag.Add("Value3", Parity.Even);
        Assert.That(bag.Get("value3")!.Equals(Parity.Even));

        bag.Add<string>("Value4", "Value");
        Assert.That(bag.Get("value4")!.Equals("Value"));
        bag.Add("Value5", 42);
        Assert.That(bag.Get("value5")!.Equals(42));
        bag.Add("Value6", Parity.Even);
        Assert.That(bag.Get("value6")!.Equals(Parity.Even));
    }

    [Test]
    public void PropertyBagSetAndUpdate() {
        var bag = new Parameters();
        bag.Add("Value1", "Value");
        Assert.That(bag.Get("value1")!.Equals("Value"));
        bag.Set("Value1", "New Value");
        Assert.That(bag.Get("value1")!.Equals("New Value"));
        bag.Set<string>("Value1", "New Value Too");
        Assert.That(bag.Get("value1")!.Equals("New Value Too"));

        bag.Add("Value2", 42);
        Assert.That(bag.Get("value2")!.Equals(42));
        bag.Set("Value2", 43);
        Assert.That(bag.Get("value2")!.Equals(43));
        bag.Set<int>("Value2", 44);
        Assert.That(bag.Get("value2")!.Equals(44));

        bag.Add("Value3", Parity.Even);
        Assert.That(bag.Get("value3")!.Equals(Parity.Even));
        bag.Set("Value3", Parity.Odd);
        Assert.That(bag.Get("value3")!.Equals(Parity.Odd));
        bag.Set<Parity>("Value3", Parity.None);
        Assert.That(bag.Get("value3")!.Equals(Parity.None));
    }

    [Test]
    public void PropertyBagDeleteTest() {
        var bag = new Parameters { { "String", "String Value" } };
        Assert.That(bag.Count == 1);
        bag.Delete("String");
        Assert.That(bag.Get("String"), Is.Null);
        Assert.That(bag.Count == 0);
    }

    [Test]
    public void PropertyBagAdd() {
        var bag = new Parameters {
            { "1", "b" },
            { "2", (object)"c" },
            { "3", (object)new string("aaa") }
        };

        Assert.That(() => bag.Add(null!, null!), Throws.Exception);
        Assert.That(() => bag.Add("5", null!), Throws.Exception);
        Assert.That(() => bag.Add(null!, (object)"aaa"), Throws.Exception);
        Assert.That(() => bag.Add("1", "b"), Throws.Exception);
        Assert.That(() => bag.Add("1", (object)"b"), Throws.Exception);
    }

    [Test]
    public void PropertyBagAddGetTest() {
        var bag = new Parameters();

        bag.Add("String", "String Value");
        Assert.That(bag.Get("String"), Is.EqualTo("String Value"));
        Assert.That(bag.Get<string>("String"), Is.EqualTo("String Value"));

        bag.Add<string>("String2", "String Value Too");
        Assert.That(bag.Get("String2"), Is.EqualTo("String Value Too"));
        Assert.That(bag.Get<string>("String2"), Is.EqualTo("String Value Too"));

        bag.Add("Number", 42);
        Assert.That(bag.Get("Number"), Is.EqualTo(42));
        Assert.That(bag.Get<long>("Number"), Is.EqualTo(42));

        bag.Add("Parity", Parity.Even);
        Assert.That(bag.Get("Parity"), Is.EqualTo(Parity.Even));
        Assert.That(bag.Get<Parity>("Parity"), Is.EqualTo(Parity.Even));
    }

    [Test]
    public void TestToString() {
        var bag = new Parameters { { "String", "String Value" } };
        var p   = bag["String"];
        Assert.That(p.ToString(), Is.EqualTo("String='String Value'"));
    }
}