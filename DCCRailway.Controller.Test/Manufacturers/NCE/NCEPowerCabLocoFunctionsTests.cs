using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Adapters.Base;
using DCCRailway.Controller.Adapters.Events;
using DCCRailway.Controller.Controllers;
using DCCRailway.Controller.NCE;
using DCCRailway.Controller.NCE.Adapters;

namespace DCCRailway.Controller.Test.Manufacturers.NCE;

[TestFixture, Ignore("This is a hardware test")]
public class NCEPowerCabLocoFunctionsTests {
    [SetUp]
    public void TestSetup() {
        var _system = new CommandStationFactory().Find("NCEPowerCab")?.Create(new NCEVirtualAdapter());

        //_system = SystemFactory.Create("NCE", "NCEPowerCab");

        if (_system != null) {
            Assert.That(_system, Is.Not.Null, "Should have an NCE PowerCab commandStation created.");
            Assert.That(_system, Is.TypeOf(typeof(NcePowerCab)), "Should be a NCE:NCEPowerCab CommandStation Created");

            Adapter = new NCEUSBSerial("COM3", 19200);
            Assert.That(Adapter, Is.Not.Null, "Should have a Serial Adapter created");
            Adapter.DataReceived  += Adapter_DataReceived;
            Adapter.DataSent      += Adapter_DataSent;
            Adapter.ErrorOccurred += Adapter_ErrorOccurred;
            _system.Adapter       =  Adapter;
        } else {
            Assert.Fail("Could not create a CommandStation Object");
        }
    }

    [TearDown]
    public void TestCleanup() {
        if (Adapter is not null) {
            Adapter.DataReceived  -= Adapter_DataReceived;
            Adapter.DataSent      -= Adapter_DataSent;
            Adapter.ErrorOccurred -= Adapter_ErrorOccurred;
            Adapter.Disconnect();
        }

        if (System != null) {
            System.Adapter = null;
            if (Adapter is not null) {
                Adapter.Dispose();
                Adapter = null;
            }
        }

        System = null;
    }
    /*
            RegisterCommand<ICmdLocoSetFunctions>(typeof(NCE.Actions.NCELocoSetFunctions));
            RegisterCommand<ICmdLocoSetSpeed>(typeof(NCE.Actions.NCELocoSetSpeed));
            RegisterCommand<ICmdLocoSetSpeedSteps>(typeof(NCE.Actions.NCELocoSetSpeedSteps));
            RegisterCommand<ICmdLocoSetMomentum>(typeof(NCE.Actions.NCELocoSetMomentum));
            RegisterCommand<ICmdLocoStop>(typeof(NCE.Actions.NCELocoStop));
    */

    protected IAdapter?        Adapter;
    protected ICommandStation? System;

    private void Adapter_ErrorOccurred(object? sender, DataErrorArgs e) => Console.WriteLine(e.ToString());

    private void Adapter_DataSent(object? sender, DataSentArgs e) => Console.WriteLine(e.ToString());

    private void Adapter_DataReceived(object? sender, DataRecvArgs e) => Console.WriteLine(e.ToString());

    [Test]
    public void TurnOnOffLightsTest() {
        if (System != null && System.Adapter != null)
            if (System.CreateCommand<ICmdLocoSetFunctions>() is ICmdLocoSetFunctions functionCmd) {
                functionCmd.Address = new DCCAddress(3, DCCAddressType.Short);

                /// Flash the headlight on/off 5 times
                for (var i = 0; i < 10; i++) {
                    functionCmd.Functions[0] = i % 2 == 0;
                    var result = functionCmd.Execute();
                    if (!result!.Success) Console.WriteLine("Did Not Work." + (char)33);

                    //Thread.Sleep(1500);
                }
            }
    }

    [Test]
    public void TurnOnOffLightsTest2() {
        if (System != null && System.Adapter != null) {
            var speedCmd = System.CreateCommand<ICmdLocoSetSpeed>();

            if (System.CreateCommand<ICmdLocoSetFunctions>() is ICmdLocoSetFunctions functionCmd) {
                functionCmd.Address = new DCCAddress(3, DCCAddressType.Short);
                speedCmd!.Address   = functionCmd.Address;

                functionCmd.Functions[0] = true;
                var result = functionCmd.Execute();
                if (!result!.Success) Console.WriteLine(result.ToString());

                speedCmd.Direction  = DCCDirection.Forward;
                speedCmd.SpeedSteps = DCCProtocol.DCC28;
                speedCmd.Speed      = new DCCSpeed(5);
                speedCmd.Execute();

                functionCmd.Functions[0] = false;
                result                   = functionCmd.Execute();
                if (!result!.Success) Console.WriteLine(result!.ToString());

                speedCmd.Direction  = DCCDirection.Forward;
                speedCmd.SpeedSteps = DCCProtocol.DCC28;
                speedCmd.Speed      = new DCCSpeed(5);
                speedCmd.Execute();

                functionCmd.Functions[0] = true;
                result                   = functionCmd.Execute();
                if (!result!.Success) Console.WriteLine(result!.ToString());

                speedCmd.Direction  = DCCDirection.Stop;
                speedCmd.SpeedSteps = DCCProtocol.DCC28;
                speedCmd.Speed      = new DCCSpeed(0);
                speedCmd.Execute();
            }
        }
    }

    [Test]
    public void SetSpeedStepsTest() {
        if (System != null && System.Adapter != null)
            if (System.CreateCommand<ICmdLocoSetSpeedSteps>() is ICmdLocoSetSpeedSteps cmd) {
                cmd.Address = new DCCAddress(3, DCCAddressType.Short);

                cmd.SpeedSteps = DCCProtocol.DCC14;
                cmd.Execute();
                Thread.Sleep(1500);

                cmd.SpeedSteps = DCCProtocol.DCC28;
                cmd.Execute();
                Thread.Sleep(1500);

                cmd.SpeedSteps = DCCProtocol.DCC128;
                cmd.Execute();
                Thread.Sleep(1500);

                cmd.SpeedSteps = DCCProtocol.DCC28;
                cmd.Execute();
                Thread.Sleep(1500);
            }
    }

    [Test]
    public void SetMomentumTest() {
        if (System != null && System.Adapter != null)
            if (System.CreateCommand<ICmdLocoSetMomentum>() is ICmdLocoSetMomentum cmd) {
                cmd.Address = new DCCAddress(3, DCCAddressType.Short);

                for (byte i = 0; i < 10; i++) {
                    cmd.Momentum = new DCCMomentum(i);
                    cmd.Execute();
                    Thread.Sleep(1500);
                }

                cmd.Momentum = new DCCMomentum(0);
                cmd.Execute();
            }
    }

    [Test]
    public void SetSpeedTest() {
        if (System != null && System.Adapter != null)
            if (System.CreateCommand<ICmdLocoSetSpeed>() is ICmdLocoSetSpeed cmd) {
                cmd.Address    = new DCCAddress(3, DCCAddressType.Short);
                cmd.Direction  = DCCDirection.Forward;
                cmd.SpeedSteps = DCCProtocol.DCC28;
                cmd.Speed      = new DCCSpeed(8);
                cmd.Execute();
                Thread.Sleep(1500);

                cmd.Direction  = DCCDirection.Forward;
                cmd.SpeedSteps = DCCProtocol.DCC28;
                cmd.Speed      = new DCCSpeed(0);
                cmd.Execute();
                Thread.Sleep(1500);

                cmd.Direction  = DCCDirection.Reverse;
                cmd.SpeedSteps = DCCProtocol.DCC28;
                cmd.Speed      = new DCCSpeed(8);
                cmd.Execute();
                Thread.Sleep(1500);

                cmd.Direction  = DCCDirection.Reverse;
                cmd.SpeedSteps = DCCProtocol.DCC28;
                cmd.Speed      = new DCCSpeed(0);
                cmd.Execute();
                Thread.Sleep(1500);
            }
    }

    [Test]
    public void SetStopTest() {
        if (System != null && System.Adapter != null)
            if (System.CreateCommand<ICmdLocoSetSpeed>() is ICmdLocoSetSpeed cmd) {
                cmd.Address    = new DCCAddress(3, DCCAddressType.Short);
                cmd.Direction  = DCCDirection.Forward;
                cmd.SpeedSteps = DCCProtocol.DCC28;
                cmd.Speed      = new DCCSpeed(8);
                cmd.Execute();
                Thread.Sleep(1500);

                var cmdStop = System.CreateCommand<ICmdLocoStop>();
                cmdStop!.Address = cmd.Address;
                cmd.Execute();
                Thread.Sleep(1500);
            }
    }
}