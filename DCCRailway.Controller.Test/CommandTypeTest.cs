using DCCRailway.Common.Helpers;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Virtual;

namespace DCCRailway.Controller.Test;

[TestFixture]
public class CommandTypeTest {
    [Test]
    public void TestThatWeCangenerateAndAcceptTypes() {
        var commandStation = new VirtualCommandStation(LoggerHelper.DebugLogger);
        commandStation.CreateAdapter("Virtual");
        var command = commandStation.CreateCommand<IDummyCmd>();
        Assert.That(command, Is.Not.Null);

        var isDummyAiDummy = command;
        Assert.That(isDummyAiDummy, Is.Not.Null);

        Assert.That(commandStation.IsCommandSupported(command.GetType()), Is.True);
        Assert.That(commandStation.IsCommandSupported<IDummyCmd>(), Is.True);
        Assert.That(commandStation.IsCommandSupported("IDummyCmd"), Is.True);
        Assert.That(commandStation.IsCommandSupported("VirtualDummyCmd"), Is.True);
    }
}