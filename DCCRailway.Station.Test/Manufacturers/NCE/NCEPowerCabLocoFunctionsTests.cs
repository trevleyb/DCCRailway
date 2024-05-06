using DCCRailway.Common.Types;
using DCCRailway.CmdStation.Adapters;
using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Adapters.Events;
using DCCRailway.CmdStation.Commands.Types;
using DCCRailway.CmdStation.Controllers;
using DCCRailway.CmdStation.NCE;
using DCCRailway.CmdStation.NCE.Adapters;

namespace DCCRailway.System.Test.Manufacturers.NCE;

[TestFixture, Ignore("This is a hardware test")]
public class NCEPowerCabLocoFunctionsTests {
    /*
            RegisterCommand<ICmdLocoSetFunctions>(typeof(NCE.Commands.NCELocoSetFunctions));
            RegisterCommand<ICmdLocoSetSpeed>(typeof(NCE.Commands.NCELocoSetSpeed));
            RegisterCommand<ICmdLocoSetSpeedSteps>(typeof(NCE.Commands.NCELocoSetSpeedSteps));
            RegisterCommand<ICmdLocoSetMomentum>(typeof(NCE.Commands.NCELocoSetMomentum));
            RegisterCommand<ICmdLocoStop>(typeof(NCE.Commands.NCELocoStop));
    */

    protected IAdapter?    Adapter;
    protected IController? System;

    [SetUp]
    public void TestSetup() {
        var _system = new ControllerFactory().Find("NCEPowerCab")?.Create(new ConsoleAdapter());

        //_system = SystemFactory.Create("NCE", "NCEPowerCab");

        if (_system != null) {
            Assert.That(_system, Is.Not.Null, "Should have an NCE PowerCab controller created.");
            Assert.That(_system, Is.TypeOf(typeof(NcePowerCab)), "Should be a NCE:NCEPowerCab Controller Created");

            Adapter = new NCEUSBSerial("COM3", 19200);
            Assert.That(Adapter, Is.Not.Null, "Should have a Serial Adapter created");
            Adapter.DataReceived  += Adapter_DataReceived;
            Adapter.DataSent      += Adapter_DataSent;
            Adapter.ErrorOccurred += Adapter_ErrorOccurred;
            _system.Adapter       =  Adapter;
        }
        else {
            Assert.Fail("Could not create a Controller Object");
        }
    }

    private void Adapter_ErrorOccurred(object? sender, DataErrorArgs e) => Console.WriteLine(e.ToString());

    private void Adapter_DataSent(object? sender, DataSentArgs e) => Console.WriteLine(e.ToString());

    private void Adapter_DataReceived(object? sender, DataRecvArgs e) => Console.WriteLine(e.ToString());

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

    [Test]
    public void TurnOnOffLightsTest() {
        if (System != null && System.Adapter != null)
            if (System.CreateCommand<ICmdLocoSetFunctions>() is ICmdLocoSetFunctions functionCmd) {
                functionCmd.Address = new DCCAddress(3, DCCAddressType.Short);

                /// Flash the headlight on/off 5 times
                for (var i = 0; i < 10; i++) {
                    functionCmd.Functions[0] = i % 2 == 0;
                    var result = System.Execute(functionCmd);
                    if (!result!.IsOK) Console.WriteLine("Did Not Work." + (char)33);

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
                var result = System.Execute(functionCmd);
                if (!result!.IsOK) Console.WriteLine(result.ToString());

                speedCmd.Direction  = DCCDirection.Forward;
                speedCmd.SpeedSteps = DCCProtocol.DCC28;
                speedCmd.Speed      = new DCCSpeed(5);
                System.Execute(speedCmd);

                functionCmd.Functions[0] = false;
                result                   = System.Execute(functionCmd);
                if (!result!.IsOK) Console.WriteLine(result!.ToString());

                speedCmd.Direction  = DCCDirection.Forward;
                speedCmd.SpeedSteps = DCCProtocol.DCC28;
                speedCmd.Speed      = new DCCSpeed(5);
                System.Execute(speedCmd);

                functionCmd.Functions[0] = true;
                result                   = System.Execute(functionCmd);
                if (!result!.IsOK) Console.WriteLine(result!.ToString());

                speedCmd.Direction  = DCCDirection.Stop;
                speedCmd.SpeedSteps = DCCProtocol.DCC28;
                speedCmd.Speed      = new DCCSpeed(0);
                System.Execute(speedCmd);
            }
        }
    }

    [Test]
    public void SetSpeedStepsTest() {
        if (System != null && System.Adapter != null)
            if (System.CreateCommand<ICmdLocoSetSpeedSteps>() is ICmdLocoSetSpeedSteps cmd) {
                cmd.Address = new DCCAddress(3, DCCAddressType.Short);

                cmd.SpeedSteps = DCCProtocol.DCC14;
                System.Execute(cmd);
                Thread.Sleep(1500);

                cmd.SpeedSteps = DCCProtocol.DCC28;
                System.Execute(cmd);
                Thread.Sleep(1500);

                cmd.SpeedSteps = DCCProtocol.DCC128;
                System.Execute(cmd);
                Thread.Sleep(1500);

                cmd.SpeedSteps = DCCProtocol.DCC28;
                System.Execute(cmd);
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
                    System.Execute(cmd);
                    Thread.Sleep(1500);
                }

                cmd.Momentum = new DCCMomentum(0);
                System.Execute(cmd);
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
                System.Execute(cmd);
                Thread.Sleep(1500);

                cmd.Direction  = DCCDirection.Forward;
                cmd.SpeedSteps = DCCProtocol.DCC28;
                cmd.Speed      = new DCCSpeed(0);
                System.Execute(cmd);
                Thread.Sleep(1500);

                cmd.Direction  = DCCDirection.Reverse;
                cmd.SpeedSteps = DCCProtocol.DCC28;
                cmd.Speed      = new DCCSpeed(8);
                System.Execute(cmd);
                Thread.Sleep(1500);

                cmd.Direction  = DCCDirection.Reverse;
                cmd.SpeedSteps = DCCProtocol.DCC28;
                cmd.Speed      = new DCCSpeed(0);
                System.Execute(cmd);
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
                System.Execute(cmd);
                Thread.Sleep(1500);

                var cmdStop = System.CreateCommand<ICmdLocoStop>();
                cmdStop!.Address = cmd.Address;
                System.Execute(cmdStop);
                Thread.Sleep(1500);
            }
    }
}