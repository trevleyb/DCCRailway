using DCCRailway.CmdStation.Actions.Commands;
using DCCRailway.CmdStation.Actions.Results;
using DCCRailway.CmdStation.Controllers;
using DCCRailway.CmdStation.NCE;
using DCCRailway.CmdStation.NCE.Adapters;

namespace DCCRailway.System.Test.Manufacturers.NCE;

[TestFixture, Ignore("This is a hardware test")]
public class NCEPowerCabTestCVReadWrite {
    [Test]
    public void SwitchMainandProg() {
        var adapter = new NCEUSBSerial("COM3", 19200);
        Assert.That(adapter, Is.Not.Null, "Should have a Serial Adapter created");

        var system = new ControllerFactory().Find("NCEPowerCab")?.Create(adapter);

        //var system = SystemFactory.Create("NCE", "NCEPowerCab", adapter);
        Assert.That(system, Is.Not.Null, "Should have an NCE PowerCab controller created.");
        Assert.That(system, Is.TypeOf(typeof(NcePowerCab)), "Should be a NCE:NCEPowerCab Controller Created");

        if (system != null && system.Adapter != null) {
            var progTrk = system.CreateCommand<ICmdTrackProg>(); //new NCESetProgTrk(adapter);
            var mainTrk = system.CreateCommand<ICmdTrackMain>(); // new NCESetMainTrk(adapter);

            var result1 = system.Execute(progTrk!);
            Assert.That(result1!.Success);
            Thread.Sleep(1000);

            var result2 = system.Execute(mainTrk!);
            Assert.That(result2!.Success);
        }
    }

    [Test]
    public void ReadCV() {
        var adapter = new NCEUSBSerial("COM3", 19200);
        Assert.That(adapter, Is.Not.Null, "Should have a Serial Adapter created");
        var system = new ControllerFactory().Find("NCEPowerCab")?.Create(adapter);

        //var system = SystemFactory.Create("NCE", "NCEPowerCab", adapter);
        Assert.That(system, Is.Not.Null, "Should have an NCE PowerCab controller created.");
        Assert.That(system, Is.TypeOf(typeof(NcePowerCab)), "Should be a NCE:NCEPowerCab Controller Created");

        if (system != null && system.Adapter != null) {
            var progTrk   = system.CreateCommand<ICmdTrackProg>(); //new NCESetProgTrk(adapter);
            var mainTrk   = system.CreateCommand<ICmdTrackMain>(); // new NCESetMainTrk(adapter);
            var readCVCmd = system.CreateCommand<ICmdCVRead>();    // new NCECVRead(01);

            //Should fail because we are not in programming mode
            //var result0 = readCVCmd.Execute(adapter);
            //Assert.IsInstanceOfType(result0, typeof(IResultOldError));
            //Assert.IsTrue(result0.OK == false);

            var result1 = system.Execute(progTrk!);
            Assert.That(result1, Is.TypeOf(typeof(ICmdResult)));
            Assert.That(result1!.Success);

            var result2 = system.Execute(readCVCmd!);
            Assert.That(result2, Is.TypeOf(typeof(ICmdResult)));
            Assert.That(result2!.Success);

            //Assert.IsTrue(((ICmdResult)result2).Value == 3);

            var result3 = system.Execute(mainTrk!);
            Assert.That(result3, Is.TypeOf(typeof(ICmdResult)));
            Assert.That(result3!.Success);
        }
    }

    [Test]
    public void ReadWriteCV() {
        var adapter = new NCEUSBSerial("COM3", 19200);
        Assert.That(adapter, Is.Not.Null, "Should have a Serial Adapter created");
        var system = new ControllerFactory().Find("NCEPowerCab")?.Create(adapter);

        //var system = SystemFactory.Create("NCE", "NCEPowerCab", adapter);
        Assert.That(system, Is.Not.Null, "Should have an NCE PowerCab controller created.");
        Assert.That(system, Is.TypeOf(typeof(NcePowerCab)), "Should be a NCE:NCEPowerCab Controller Created");

        if (system != null && system.Adapter != null) {
            var progTrk    = system.CreateCommand<ICmdTrackProg>();
            var mainTrk    = system.CreateCommand<ICmdTrackMain>();
            var readCVCmd  = system.CreateCommand<ICmdCVRead>();
            var writeCVCmd = system.CreateCommand<ICmdCVWrite>();

            // Should fail because we are not in programming mode
            var result0 = system.Execute(readCVCmd!);
            Assert.That(result0, Is.TypeOf(typeof(ICmdResult)));
            Assert.That(result0!.Success == false);

            var result1 = system.Execute(progTrk!);
            Assert.That(result1, Is.TypeOf(typeof(ICmdResult)));
            Assert.That(result1!.Success);

            var result2 = system.Execute(readCVCmd!);
            Assert.That(result2, Is.TypeOf(typeof(ICmdResult)));
            Assert.That(result2!.Success);
            Assert.That(result2.Byte == 3);

            writeCVCmd!.Value = 67;
            var result3 = system.Execute(writeCVCmd);
            Assert.That(result3, Is.TypeOf(typeof(ICmdResult)));
            Assert.That(result3!.Success);

            var result4 = system.Execute(readCVCmd!);
            Assert.That(result4, Is.TypeOf(typeof(ICmdResult)));
            Assert.That(result4!.Success);
            Assert.That(result4.Byte == 67);

            writeCVCmd!.Value = result2.Byte;
            var result5 = system.Execute(writeCVCmd);
            Assert.That(result5, Is.TypeOf(typeof(ICmdResult)));
            Assert.That(result5!.Success);

            var result6 = system.Execute(readCVCmd!);
            Assert.That(result6, Is.TypeOf(typeof(ICmdResult)));
            Assert.That(result6!.Success);
            Assert.That(result6.Byte == result2.Byte);

            var result7 = system.Execute(mainTrk!);
            Assert.That(result7, Is.TypeOf(typeof(ICmdResult)));
            Assert.That(result7!.Success, Is.True);
        }
    }
}