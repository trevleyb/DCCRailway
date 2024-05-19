using System.Text.Json.Serialization;
using DCCRailway.Common.Helpers;
using DCCRailway.Layout.Base;
using DCCRailway.Layout.Collection;
using DCCRailway.Layout.Entities;
using Newtonsoft.Json;
using NUnit.Framework;
using JsonConverter = System.Text.Json.Serialization.JsonConverter;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace DCCRailway.Test;

[TestFixture]
public class TestSerialize {

    [Test]
    public void TestComplex() {

        var tr = new TestRepository();
        tr.HeaderValue = "Some Data in the Header";
        tr.Amount = 1234.5678;
        tr.isTrue = false;
        tr.Add(new TestEntity() { Tempvalue = "Value1" });
        tr.Add(new TestEntity() { Tempvalue = "Value2" });
        tr.Add(new TestEntity() { Tempvalue = "Value3" });
        tr.Add(new TestEntity() { Tempvalue = "Value4" });
        tr.Add(new TestEntity() { Tempvalue = "Value5" });

        var aa = new Accessories();
        var ss = LayoutStorage.SerializeLayout<TestRepository, TestEntity>(tr);

        var dd = LayoutStorage.DeserializeLayout<TestRepository, TestEntity>(ss);
        Console.WriteLine(dd.HeaderValue, tr.HeaderValue);

    }
}

[Serializable]
public class TestRepository : LayoutRepository<TestEntity> {
    public string HeaderValue { get; set; }
    public double Amount { get; set; }
    public bool isTrue { get; set; }
    public object? CanBeNull { get; set; }
}

[Serializable]
public class TestEntity : LayoutEntity {
    public string Tempvalue { get; set; }
}