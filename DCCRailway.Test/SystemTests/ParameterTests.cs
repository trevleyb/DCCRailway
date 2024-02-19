using System.IO.Ports;
using DCCRailway.Configuration;
using NUnit.Framework;

namespace DCCRailway.Test.SystemTests;

[TestFixture]
public class ParameterTests {

    [Test]
    public void TypeConvertTest() {

        int var1                      = 42;
        var typeName                  = var1.GetType().Name;
        var typeFullName              = var1.GetType().FullName;
        var typeAssemblyQualifiedName = var1.GetType().AssemblyQualifiedName;
        var typeToString              = var1.GetType().ToString();

        var convertName                  = Type.GetType(typeName);
        var convertFullName              = Type.GetType(typeFullName);
        var convertAssemblyQualifiedName = Type.GetType(typeAssemblyQualifiedName);
        var convertToString              = Type.GetType(typeToString);

    }

    [Test]
    public void PropertyBagNullExceptionTest() {

        var bag = new Parameters();
        Assert.That(() => bag.Add(null!, null!), Throws.Exception);
        Assert.That(() => bag.Add("Empty", null!), Throws.Exception);
        Assert.That(() => bag.Add<String>(null!, null!), Throws.Exception);
        Assert.That(() => bag.Add<String>("Empty", null!), Throws.Exception);
        Assert.That(() => bag.Set(null!, "Value"), Throws.Exception);
        Assert.That(() => bag.Set<String>(null!, "Value"), Throws.Exception);
        Assert.That(() => bag.Set("Empty", null!), Throws.Exception);
        Assert.That(() => bag.Set<String>("Empty", null!), Throws.Exception);
        Assert.That(() => bag.Get(null!), Throws.Exception);
        Assert.That(() => bag.Get<String>(null!), Throws.Exception);
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
    public void PropertyBagAddTest() {

        var bag = new Parameters();

        bag.Add("String", "String Value");
        Assert.That(bag.Get("String"), Is.EqualTo("String Value"));
        Assert.That(bag.Get<String>("String"), Is.EqualTo("String Value"));

        bag.Add<String>("String2", "String Value Too");
        Assert.That(bag.Get("String2"), Is.EqualTo("String Value Too"));
        Assert.That(bag.Get<String>("String2"), Is.EqualTo("String Value Too"));

        bag.Add("Number", 42);
        Assert.That(bag.Get("Number"), Is.EqualTo(42));
        Assert.That(bag.Get<long>("Number"), Is.EqualTo(42));

        bag.Add("Parity", Parity.Even);
        Assert.That(bag.Get("Parity"), Is.EqualTo(Parity.Even));
        Assert.That(bag.Get<Parity>("Parity"), Is.EqualTo(Parity.Even));
    }
}