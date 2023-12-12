using DCCRailway.System;
using DCCRailway.System.Commands.CommandType;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.NCE;
using DCCRailway.System.NCE.Adapters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DCCRailway.Test;

[TestClass]
public class NCEPowerCabTestCVReadWrite {
    [TestMethod]
    public void SwitchMainandProg() {
        var adapter = new NCEUSBSerial("COM3", 19200);
        Assert.IsNotNull(adapter, "Should have a Serial Adapter created");
        var system = SystemFactory.Create("NCE", "PowerCab", adapter);
        Assert.IsNotNull(system, "Should have an NCE PowerCab system created.");
        Assert.IsInstanceOfType(system, typeof(NcePowerCab), "Should be a NCE:NCEPowerCab System Created");

        if (system != null && system.Adapter != null) {
            var progTrk = system.CreateCommand<ICmdTrackProg>(); //new NCESetProgTrk(adapter);
            var mainTrk = system.CreateCommand<ICmdTrackMain>(); // new NCESetMainTrk(adapter);

            var result1 = system.Execute(progTrk!);
            Assert.IsTrue(result1!.IsOK);
            Thread.Sleep(1000);

            var result2 = system.Execute(mainTrk!);
            Assert.IsTrue(result2!.IsOK);
        }
    }

    [TestMethod]
    public void ReadCV() {
        var adapter = new NCEUSBSerial("COM3", 19200);
        Assert.IsNotNull(adapter, "Should have a Serial Adapter created");
        var system = SystemFactory.Create("NCE", "PowerCab", adapter);
        Assert.IsNotNull(system, "Should have an NCE PowerCab system created.");
        Assert.IsInstanceOfType(system, typeof(NcePowerCab), "Should be a NCE:NCEPowerCab System Created");

        if (system != null && system.Adapter != null) {
            var progTrk   = system.CreateCommand<ICmdTrackProg>(); //new NCESetProgTrk(adapter);
            var mainTrk   = system.CreateCommand<ICmdTrackMain>(); // new NCESetMainTrk(adapter);
            var readCVCmd = system.CreateCommand<ICmdCVRead>(01);  // new NCECVRead(01);

            //Should fail because we are not in programming mode
            //var result0 = readCVCmd.Execute(adapter);
            //Assert.IsInstanceOfType(result0, typeof(IResultOldError));
            //Assert.IsTrue(result0.OK == false);

            var result1 = system.Execute(progTrk!);
            Assert.IsInstanceOfType(result1, typeof(ICommandResult));
            Assert.IsTrue(result1!.IsOK);

            var result2 = system.Execute(readCVCmd!);
            Assert.IsInstanceOfType(result2, typeof(ICommandResult));
            Assert.IsTrue(result2!.IsOK);
            //Assert.IsTrue(((ICommandResult)result2).Value == 3);

            var result3 = system.Execute(mainTrk!);
            Assert.IsInstanceOfType(result3, typeof(ICommandResult));
            Assert.IsTrue(result3!.IsOK);
        }
    }

    [TestMethod]
    public void ReadWriteCV() {
        var adapter = new NCEUSBSerial("COM3", 19200);
        Assert.IsNotNull(adapter, "Should have a Serial Adapter created");
        var system = SystemFactory.Create("NCE", "PowerCab", adapter);
        Assert.IsNotNull(system, "Should have an NCE PowerCab system created.");
        Assert.IsInstanceOfType(system, typeof(NcePowerCab), "Should be a NCE:NCEPowerCab System Created");

        if (system != null && system.Adapter != null) {
            var progTrk    = system.CreateCommand<ICmdTrackProg>();
            var mainTrk    = system.CreateCommand<ICmdTrackMain>();
            var readCVCmd  = system.CreateCommand<ICmdCVRead>(01);
            var writeCVCmd = system.CreateCommand<ICmdCVWrite>(01);

            // Should fail because we are not in programming mode
            var result0 = system.Execute(readCVCmd!);
            Assert.IsInstanceOfType(result0, typeof(ICommandResult));
            Assert.IsTrue(result0!.IsOK == false);

            var result1 = system.Execute(progTrk!);
            Assert.IsInstanceOfType(result1, typeof(ICommandResult));
            Assert.IsTrue(result1!.IsOK);

            var result2 = system.Execute(readCVCmd!);
            Assert.IsInstanceOfType(result2, typeof(ICommandResult));
            Assert.IsTrue(result2!.IsOK);
            Assert.IsTrue(result2.Byte == 3);

            writeCVCmd!.Value = 67;
            var result3 = system.Execute(writeCVCmd);
            Assert.IsInstanceOfType(result3, typeof(ICommandResult));
            Assert.IsTrue(result3!.IsOK);

            var result4 = system.Execute(readCVCmd!);
            Assert.IsInstanceOfType(result4, typeof(ICommandResult));
            Assert.IsTrue(result4!.IsOK);
            Assert.IsTrue(result4.Byte == 67);

            writeCVCmd!.Value = result2.Byte;
            var result5 = system.Execute(writeCVCmd);
            Assert.IsInstanceOfType(result5, typeof(ICommandResult));
            Assert.IsTrue(result5!.IsOK);

            var result6 = system.Execute(readCVCmd!);
            Assert.IsInstanceOfType(result6, typeof(ICommandResult));
            Assert.IsTrue(result6!.IsOK);
            Assert.IsTrue(result6.Byte == result2.Byte);

            var result7 = system.Execute(mainTrk!);
            Assert.IsInstanceOfType(result7, typeof(ICommandResult));
            Assert.IsTrue(result7!.IsOK);
        }
    }
}