using DCCRailway.System.Commands.Results;
using DCCRailway.System.Commands.Types;
using DCCRailway.System.Controllers;
using DCCRailway.Manufacturer.NCE;
using DCCRailway.Manufacturer.NCE.Adapters;
using NUnit.Framework;

namespace DCCRailway.Test.Manufacturers.NCE;

[TestFixture, Ignore("This is a hardware test")]
public class NCEPowerCabTestCVReadWrite {
    [Test]
    public void SwitchMainandProg() {
        var adapter = new NCEUSBSerial("COM3", 19200);
        Assert.That(adapter, Is.Not.Null,"Should have a Serial Adapter created");
        
        var system = new ControllerFactory().Find("NCEPowerCab")?.Create(adapter);
        //var system = SystemFactory.Create("NCE", "NCEPowerCab", adapter);
        Assert.That(system, Is.Not.Null,"Should have an NCE PowerCab controller created.");
        Assert.That(system, Is.TypeOf(typeof(NcePowerCab)), "Should be a NCE:NCEPowerCab Controller Created");

        if (system != null && system.Adapter != null) {
            var progTrk = system.CreateCommand<ICmdTrackProg>(); //new NCESetProgTrk(adapter);
            var mainTrk = system.CreateCommand<ICmdTrackMain>(); // new NCESetMainTrk(adapter);

            var result1 = system.Execute(progTrk!);
            Assert.That(result1!.IsOK);
            Thread.Sleep(1000);

            var result2 = system.Execute(mainTrk!);
            Assert.That(result2!.IsOK);
        }
    }

    [Test]
    public void ReadCV() {
        var adapter = new NCEUSBSerial("COM3", 19200);
        Assert.That(adapter, Is.Not.Null,"Should have a Serial Adapter created");
        var system = new ControllerFactory().Find("NCEPowerCab")?.Create(adapter);
        //var system = SystemFactory.Create("NCE", "NCEPowerCab", adapter);
        Assert.That(system, Is.Not.Null,"Should have an NCE PowerCab controller created.");
        Assert.That(system, Is.TypeOf(typeof(NcePowerCab)), "Should be a NCE:NCEPowerCab Controller Created");

        if (system != null && system.Adapter != null) {
            var progTrk   = system.CreateCommand<ICmdTrackProg>(); //new NCESetProgTrk(adapter);
            var mainTrk   = system.CreateCommand<ICmdTrackMain>(); // new NCESetMainTrk(adapter);
            var readCVCmd = system.CreateCommand<ICmdCVRead>();  // new NCECVRead(01);

            //Should fail because we are not in programming mode
            //var result0 = readCVCmd.Execute(adapter);
            //Assert.IsInstanceOfType(result0, typeof(IResultOldError));
            //Assert.IsTrue(result0.OK == false);

            var result1 = system.Execute(progTrk!);
            Assert.That(result1, Is.TypeOf(typeof(ICommandResult)));
            Assert.That(result1!.IsOK);

            var result2 = system.Execute(readCVCmd!);
            Assert.That(result2, Is.TypeOf(typeof(ICommandResult)));
            Assert.That(result2!.IsOK);
            //Assert.IsTrue(((ICommandResult)result2).Value == 3);

            var result3 = system.Execute(mainTrk!);
            Assert.That(result3, Is.TypeOf(typeof(ICommandResult)));
            Assert.That(result3!.IsOK);
        }
    }

    [Test]
    public void ReadWriteCV() {
        var adapter = new NCEUSBSerial("COM3", 19200);
        Assert.That(adapter, Is.Not.Null, "Should have a Serial Adapter created");
        var system = new ControllerFactory().Find("NCEPowerCab")?.Create(adapter);
        //var system = SystemFactory.Create("NCE", "NCEPowerCab", adapter);
        Assert.That(system, Is.Not.Null,"Should have an NCE PowerCab controller created.");
        Assert.That(system, Is.TypeOf(typeof(NcePowerCab)), "Should be a NCE:NCEPowerCab Controller Created");

        if (system != null && system.Adapter != null) {
            var progTrk    = system.CreateCommand<ICmdTrackProg>();
            var mainTrk    = system.CreateCommand<ICmdTrackMain>();
            var readCVCmd  = system.CreateCommand<ICmdCVRead>();
            var writeCVCmd = system.CreateCommand<ICmdCVWrite>();

            // Should fail because we are not in programming mode
            var result0 = system.Execute(readCVCmd!);
            Assert.That(result0, Is.TypeOf(typeof(ICommandResult)));
            Assert.That(result0!.IsOK == false);

            var result1 = system.Execute(progTrk!);
            Assert.That(result1, Is.TypeOf(typeof(ICommandResult)));
            Assert.That(result1!.IsOK);

            var result2 = system.Execute(readCVCmd!);
            Assert.That(result2, Is.TypeOf(typeof(ICommandResult)));
            Assert.That(result2!.IsOK);
            Assert.That(result2.Byte == 3);

            writeCVCmd!.Value = 67;
            var result3 = system.Execute(writeCVCmd);
            Assert.That(result3, Is.TypeOf(typeof(ICommandResult)));
            Assert.That(result3!.IsOK);

            var result4 = system.Execute(readCVCmd!);
            Assert.That(result4, Is.TypeOf(typeof(ICommandResult)));
            Assert.That(result4!.IsOK);
            Assert.That(result4.Byte == 67);

            writeCVCmd!.Value = result2.Byte;
            var result5 = system.Execute(writeCVCmd);
            Assert.That(result5, Is.TypeOf(typeof(ICommandResult)));
            Assert.That(result5!.IsOK);

            var result6 = system.Execute(readCVCmd!);
            Assert.That(result6, Is.TypeOf(typeof(ICommandResult)));
            Assert.That(result6!.IsOK);
            Assert.That(result6.Byte == result2.Byte);

            var result7 = system.Execute(mainTrk!);
            Assert.That(result7, Is.TypeOf(typeof(ICommandResult)));
            Assert.That(result7!.IsOK,Is.True);
        }
    }
}