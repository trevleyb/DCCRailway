using DCCRailway.Manufacturer.NCE;
using DCCRailway.Manufacturer.NCE.Adapters;
using DCCRailway.System.Adapters;
using DCCRailway.System.Adapters.Events;
using DCCRailway.System.Commands.Types;
using DCCRailway.System.Controllers;
using DCCRailway.System.Types;
using NUnit.Framework;

namespace DCCRailway.Test.Manufacturers.NCE;

[TestFixture]
public class NCEPowerCabLocoFunctionsTests {
    /*
            RegisterCommand<ICmdLocoSetFunctions>(typeof(NCE.Commands.NCELocoSetFunctions));
            RegisterCommand<ICmdLocoSetSpeed>(typeof(NCE.Commands.NCELocoSetSpeed));
            RegisterCommand<ICmdLocoSetSpeedSteps>(typeof(NCE.Commands.NCELocoSetSpeedSteps));
            RegisterCommand<ICmdLocoSetMomentum>(typeof(NCE.Commands.NCELocoSetMomentum));
            RegisterCommand<ICmdLocoStop>(typeof(NCE.Commands.NCELocoStop));
    */

    private IAdapter _adapter;
    private IController? _system;

    [SetUp]
    public void TestSetup() {
        var _system = new ControllerFactory().Find("NCEPowerCab")?.Create(new ConsoleAdapter());
        //_system = SystemFactory.Create("NCE", "NCEPowerCab");

        if (_system != null) {
            Assert.That(_system, Is.Not.Null,"Should have an NCE PowerCab controller created.");
            Assert.That(_system, Is.TypeOf(typeof(NcePowerCab)), "Should be a NCE:NCEPowerCab Controller Created");

            _adapter = new NCEUSBSerial("COM3", 19200);
            Assert.That(_adapter,Is.Not.Null, "Should have a Serial Adapter created");
            _adapter.DataReceived            += Adapter_DataReceived;
            _adapter.DataSent                += Adapter_DataSent;
            _adapter.ErrorOccurred           += Adapter_ErrorOccurred;
            _system.Adapter                  =  _adapter;
        } else {
            Assert.Fail("Could not create a Controller Object");
        }
    }

    private void Adapter_ErrorOccurred(object? sender, ErrorArgs e) => Console.WriteLine(e.ToString());
    
    private void Adapter_DataSent(object? sender, DataSentArgs e) => Console.WriteLine(e.ToString());

    private void Adapter_DataReceived(object? sender, DataRecvArgs e) => Console.WriteLine(e.ToString());

    [TearDown]
    public void TestCleanup() {
        _adapter.DataReceived            -= Adapter_DataReceived;
        _adapter.DataSent                -= Adapter_DataSent;
        _adapter.ErrorOccurred           -= Adapter_ErrorOccurred;
        _adapter.Disconnect();
    }

    [Test]
    public void TurnOnOffLightsTest() {
        if (_system != null && _system.Adapter != null) {
            if (_system.CreateCommand<ICmdLocoSetFunctions>() is ICmdLocoSetFunctions functionCmd) {
                functionCmd.Address = new DCCAddress(3, DCCAddressType.Short);

                /// Flash the headlight on/off 5 times
                for (var i = 0; i < 10; i++) {
                    functionCmd.Functions[0] = i % 2 == 0;
                    var result = _system.Execute(functionCmd);
                    if (!result!.IsOK) Console.WriteLine("Did Not Work." + (char)33);

                    //Thread.Sleep(1500);
                }
            }
        }
    }

    [Test]
    public void TurnOnOffLightsTest2() {
        if (_system != null && _system.Adapter != null) {
            var speedCmd = _system.CreateCommand<ICmdLocoSetSpeed>();

            if (_system.CreateCommand<ICmdLocoSetFunctions>() is ICmdLocoSetFunctions functionCmd) {
                functionCmd.Address = new DCCAddress(3, DCCAddressType.Short);
                speedCmd!.Address   = functionCmd.Address;

                functionCmd.Functions[0] = true;
                var result = _system.Execute(functionCmd);
                if (!result!.IsOK) Console.WriteLine(result.ToString());

                speedCmd.Direction  = DCCDirection.Forward;
                speedCmd.SpeedSteps = DCCProtocol.DCC28;
                speedCmd.Speed      = 5;
                _system.Execute(speedCmd);

                functionCmd.Functions[0] = false;
                result                   = _system.Execute(functionCmd);
                if (!result!.IsOK) Console.WriteLine(result!.ToString());

                speedCmd.Direction  = DCCDirection.Forward;
                speedCmd.SpeedSteps = DCCProtocol.DCC28;
                speedCmd.Speed      = 5;
                _system.Execute(speedCmd);

                functionCmd.Functions[0] = true;
                result                   = _system.Execute(functionCmd);
                if (!result!.IsOK) Console.WriteLine(result!.ToString());

                speedCmd.Direction  = DCCDirection.Stop;
                speedCmd.SpeedSteps = DCCProtocol.DCC28;
                speedCmd.Speed      = 0;
                _system.Execute(speedCmd);
            }
        }
    }

    [Test]
    public void SetSpeedStepsTest() {
        if (_system != null && _system.Adapter != null) {
            if (_system.CreateCommand<ICmdLocoSetSpeedSteps>() is ICmdLocoSetSpeedSteps cmd) {
                cmd.Address = new DCCAddress(3, DCCAddressType.Short);

                cmd.SpeedSteps = DCCProtocol.DCC14;
                _system.Execute(cmd);
                Thread.Sleep(1500);

                cmd.SpeedSteps = DCCProtocol.DCC28;
                _system.Execute(cmd);
                Thread.Sleep(1500);

                cmd.SpeedSteps = DCCProtocol.DCC128;
                _system.Execute(cmd);
                Thread.Sleep(1500);

                cmd.SpeedSteps = DCCProtocol.DCC28;
                _system.Execute(cmd);
                Thread.Sleep(1500);
            }
        }
    }

    [Test]
    public void SetMomentumTest() {
        if (_system != null && _system.Adapter != null) {
            if (_system.CreateCommand<ICmdLocoSetMomentum>() is ICmdLocoSetMomentum cmd) {
                cmd.Address = new DCCAddress(3, DCCAddressType.Short);

                for (byte i = 0; i < 10; i++) {
                    cmd.Momentum = i;
                    _system.Execute(cmd);
                    Thread.Sleep(1500);
                }

                cmd.Momentum = 0;
                _system.Execute(cmd);
            }
        }
    }

    [Test]
    public void SetSpeedTest() {
        if (_system != null && _system.Adapter != null) {
            if (_system.CreateCommand<ICmdLocoSetSpeed>() is ICmdLocoSetSpeed cmd) {
                cmd.Address    = new DCCAddress(3, DCCAddressType.Short);
                cmd.Direction  = DCCDirection.Forward;
                cmd.SpeedSteps = DCCProtocol.DCC28;
                cmd.Speed      = 8;
                _system.Execute(cmd);
                Thread.Sleep(1500);

                cmd.Direction  = DCCDirection.Forward;
                cmd.SpeedSteps = DCCProtocol.DCC28;
                cmd.Speed      = 0;
                _system.Execute(cmd);
                Thread.Sleep(1500);

                cmd.Direction  = DCCDirection.Reverse;
                cmd.SpeedSteps = DCCProtocol.DCC28;
                cmd.Speed      = 8;
                _system.Execute(cmd);
                Thread.Sleep(1500);

                cmd.Direction  = DCCDirection.Reverse;
                cmd.SpeedSteps = DCCProtocol.DCC28;
                cmd.Speed      = 0;
                _system.Execute(cmd);
                Thread.Sleep(1500);
            }
        }
    }

    [Test]
    public void SetStopTest() {
        if (_system != null && _system.Adapter != null) {
            if (_system.CreateCommand<ICmdLocoSetSpeed>() is ICmdLocoSetSpeed cmd) {
                cmd.Address    = new DCCAddress(3, DCCAddressType.Short);
                cmd.Direction  = DCCDirection.Forward;
                cmd.SpeedSteps = DCCProtocol.DCC28;
                cmd.Speed      = 8;
                _system.Execute(cmd);
                Thread.Sleep(1500);

                var cmdStop = _system.CreateCommand<ICmdLocoStop>();
                cmdStop!.Address = cmd.Address;
                _system.Execute(cmdStop);
                Thread.Sleep(1500);
            }
        }
    }
}