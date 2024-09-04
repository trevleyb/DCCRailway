using System.Text.Json;
using System.Text.Json.Serialization;
using DCCRailway.Common.Entities;
using DCCRailway.Common.Types;
using Parameter = DCCRailway.Common.Configuration.Parameter;

namespace DCCRailway.Common.Test;

[TestFixture]
public class LocomotiveTest {
    [Test]
    public void TestLocomotive() {
        var loco = new Locomotive {
            Name        = "MyLoco",
            Description = "My Locomotive"
        };

        Assert.That(loco, Is.Not.Null);
        Assert.That(loco.Name.Equals("MyLoco"));
    }

    [Test]
    public void SerializeAndDeserializeLocos() {
        var locos = new Locomotives();
        locos.Add(new Locomotive { Name = "MyLoco1", Description = "My Locomotive1", Id = "AA01", Address = new DCCAddress(100), Speed = new DCCSpeed(10) });
        locos.Add(new Locomotive { Name = "MyLoco2", Description = "My Locomotive2", Id = "AA22", Address = new DCCAddress(110), Speed = new DCCSpeed(20) });
        locos.Add(new Locomotive { Name = "MyLoco3", Description = "My Locomotive3", Id = "AA31", Address = new DCCAddress(120), Speed = new DCCSpeed(30) });
        locos.Add(new Locomotive { Name = "MyLoco4", Description = "My Locomotive4", Id = "AA41", Address = new DCCAddress(130), Speed = new DCCSpeed(40) });
        locos.Add(new Locomotive { Name = "MyLoco5", Description = "My Locomotive5", Id = "AA51", Address = new DCCAddress(140), Speed = new DCCSpeed(50) });

        var options = new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition      = JsonIgnoreCondition.WhenWritingNull
        };

        try {
            var json = JsonSerializer.Serialize(locos, options);
            var obj  = JsonSerializer.Deserialize<Locomotives>(json, options) as Locomotives;
            foreach (var loco in locos) {
                Assert.That(loco.Name, Is.EqualTo(obj?[loco.Id].Name));
            }
        } catch (Exception e) {
            Console.Error.WriteLine(e);
        }
    }

    [Test]
    public void SerializeAndDeserializeParameters() {
        var parameters = new Configuration.Parameters();
        parameters.Add(new Parameter("Name", "MyLoco"));
        parameters.Add(new Parameter("Age", 27));
        parameters.Add(new Parameter("Bool", true));
        parameters.Add(new Parameter("Value", 167.99));

        var json = JsonSerializer.Serialize(parameters);
        var obj  = JsonSerializer.Deserialize<Entities.Parameters>(json);

        foreach (var parameter in parameters) {
            var value = obj?[parameter.Name];
            Assert.That(parameter.Name, Is.EqualTo(value?.Name));
        }
    }
}