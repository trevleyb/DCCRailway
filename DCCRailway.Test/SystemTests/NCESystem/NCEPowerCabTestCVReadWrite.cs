using DCCRailway.System;
using DCCRailway.System.Commands.Interfaces;
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
            Assert.IsTrue(result1!.OK);
            Thread.Sleep(1000);

            var result2 = system.Execute(mainTrk!);
            Assert.IsTrue(result2!.OK);
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
            //Assert.IsInstanceOfType(result0, typeof(IResultError));
            //Assert.IsTrue(result0.OK == false);

            var result1 = system.Execute(progTrk!);
            Assert.IsInstanceOfType(result1, typeof(IResultOK));
            Assert.IsTrue(result1!.OK);

            var result2 = system.Execute(readCVCmd!);
            Assert.IsInstanceOfType(result2, typeof(IResultOK));
            Assert.IsTrue(result2!.OK);
            Assert.IsTrue(((IResultOK)result2).Value == 3);

            var result3 = system.Execute(mainTrk!);
            Assert.IsInstanceOfType(result3, typeof(IResultOK));
            Assert.IsTrue(result3!.OK);
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
            Assert.IsInstanceOfType(result0, typeof(IResultError));
            Assert.IsTrue(result0!.OK == false);

            var result1 = system.Execute(progTrk!);
            Assert.IsInstanceOfType(result1, typeof(IResultOK));
            Assert.IsTrue(result1!.OK);

            var result2 = system.Execute(readCVCmd!);
            Assert.IsInstanceOfType(result2, typeof(IResultOK));
            Assert.IsTrue(result2!.OK);
            Assert.IsTrue(result2.Value == 3);

            writeCVCmd!.Value = 67;
            var result3 = system.Execute(writeCVCmd);
            Assert.IsInstanceOfType(result3, typeof(IResultOK));
            Assert.IsTrue(result3!.OK);

            var result4 = system.Execute(readCVCmd!);
            Assert.IsInstanceOfType(result4, typeof(IResultOK));
            Assert.IsTrue(result4!.OK);
            Assert.IsTrue(result4.Value == 67);

            writeCVCmd!.Value = result2.Value ?? 0;
            var result5 = system.Execute(writeCVCmd);
            Assert.IsInstanceOfType(result5, typeof(IResultOK));
            Assert.IsTrue(result5!.OK);

            var result6 = system.Execute(readCVCmd!);
            Assert.IsInstanceOfType(result6, typeof(IResultOK));
            Assert.IsTrue(result6!.OK);
            Assert.IsTrue(result6.Value == result2.Value);

            var result7 = system.Execute(mainTrk!);
            Assert.IsInstanceOfType(result7, typeof(IResultOK));
            Assert.IsTrue(result7!.OK);
        }
    }
}