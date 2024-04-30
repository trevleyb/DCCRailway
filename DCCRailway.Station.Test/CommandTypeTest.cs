using DCCRailway.Station.Commands.Types;
using DCCRailway.Station.Digitrax.Commands;
using DCCRailway.Station.Helpers;
using DCCRailway.Station.Virtual;
using NUnit.Framework.Interfaces;

namespace DCCRailway.System.Test;

[TestFixture]
public class CommandTypeTest {

    [TestCase]
    public void TestThatWeCangenerateAndAcceptTypes() {

        var commandStation = new VirtualController();
        var command = commandStation.CreateCommand<IDummyCmd>();
        Assert.That(command,Is.Not.Null);

        var isDummyAiDummy = command as IDummyCmd;
        Assert.That(isDummyAiDummy, Is.Not.Null);

        Assert.That(commandStation.IsCommandSupported(command.GetType()),Is.True);
        Assert.That(commandStation.IsCommandSupported<IDummyCmd>(),Is.True);
        Assert.That(commandStation.IsCommandSupported("IDummyCmd"),Is.True);
        Assert.That(commandStation.IsCommandSupported("VirtualDummyCmd"),Is.True);
    }
}