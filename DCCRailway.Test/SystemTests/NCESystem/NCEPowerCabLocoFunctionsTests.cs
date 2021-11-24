using DCCRailway.Core;
using DCCRailway.Core.Adapters;
using DCCRailway.Core.Commands;
using DCCRailway.Core.Common;
using DCCRailway.Core.Events;
using DCCRailway.Systems.NCE.Adapters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DCCRailway.Test {
	[TestClass]
	public class NCEPowerCabLocoFunctionsTests {
		/*
		        Register<ICmdLocoSetFunctions>(typeof(NCE.Commands.NCELocoSetFunctions));
		        Register<ICmdLocoSetSpeed>(typeof(NCE.Commands.NCELocoSetSpeed));
		        Register<ICmdLocoSetSpeedSteps>(typeof(NCE.Commands.NCELocoSetSpeedSteps));
		        Register<ICmdLocoSetMomentum>(typeof(NCE.Commands.NCELocoSetMomentum));
		        Register<ICmdLocoStop>(typeof(NCE.Commands.NCELocoStop));
		*/

		private IAdapter _adapter;
		private ISystem? _system;

		[TestInitialize]
		public void TestSetup() {
			_system = SystemFactory.Create("NCE", "PowerCab");
			if (_system != null) {
				Assert.IsNotNull(_system, "Should have an NCE PowerCab system created.");
				Assert.IsInstanceOfType(_system, typeof(Systems.NCE.NCEPowerCab), "Should be a NCE:NCEPowerCab System Created");

				_adapter = new NCEUSBSerial("COM3", 19200);
				Assert.IsNotNull(_adapter, "Should have a Serial Adapter created");
				_adapter.DataReceived += Adapter_DataReceived;
				_adapter.DataSent += Adapter_DataSent;
				_adapter.ConnectionStatusChanged += Adapter_ConnectionStatusChanged;
				_adapter.ErrorOccurred += Adapter_ErrorOccurred;
				_system.Adapter = _adapter;
			} else
				Assert.Fail("Could not create a System Object");
		}

		private void Adapter_ErrorOccurred(object? sender, ErrorArgs e) {
			Console.WriteLine(e.ToString());
		}

		private void Adapter_ConnectionStatusChanged(object? sender, StateChangedArgs e) {
			Console.WriteLine(e.ToString());
		}

		private void Adapter_DataSent(object? sender, DataSentArgs e) {
			Console.WriteLine(e.ToString());
		}

		private void Adapter_DataReceived(object? sender, DataRecvArgs e) {
			Console.WriteLine(e.ToString());
		}

		[TestCleanup]
		public void TestCleanup() {
			_adapter.DataReceived -= Adapter_DataReceived;
			_adapter.DataSent -= Adapter_DataSent;
			_adapter.ConnectionStatusChanged -= Adapter_ConnectionStatusChanged;
			_adapter.ErrorOccurred -= Adapter_ErrorOccurred;
			_adapter.Disconnect();
		}

		[TestMethod]
		public void TurnOnOffLightsTest() {
			if (_system != null && _system.Adapter != null) {
				if (_system.CreateCommand<ICmdLocoSetFunctions>() is ICmdLocoSetFunctions functionCmd) {
					functionCmd.Address = new DCCAddress(3, DCCAddressType.Short);

					/// Flash the headlight on/off 5 times
					for (var i = 0; i < 10; i++) {
						functionCmd.Functions[0] = i % 2 == 0;
						var result = _system.Execute(functionCmd);
						if (!result!.OK) Console.WriteLine("Did Not Work." + (char)33);

						//Thread.Sleep(1500);
					}
				}
			}
		}

		[TestMethod]
		public void TurnOnOffLightsTest2() {
			if (_system != null && _system.Adapter != null) {
				var speedCmd = _system.CreateCommand<ICmdLocoSetSpeed>();
				if (_system.CreateCommand<ICmdLocoSetFunctions>() is ICmdLocoSetFunctions functionCmd) {
					functionCmd.Address = new DCCAddress(3, DCCAddressType.Short);
					speedCmd!.Address = functionCmd.Address;

					functionCmd.Functions[0] = true;
					var result = _system.Execute(functionCmd);
					if (!result!.OK) Console.WriteLine(((IResultError)result)!.ToString());

					speedCmd.Direction = DCCDirection.Forward;
					speedCmd.SpeedSteps = DCCProtocol.DCC28;
					speedCmd.Speed = 5;
					_system.Execute(speedCmd);

					functionCmd.Functions[0] = false;
					result = _system.Execute(functionCmd);
					if (!result!.OK) Console.WriteLine(((IResultError)result)!.ToString());

					speedCmd.Direction = DCCDirection.Forward;
					speedCmd.SpeedSteps = DCCProtocol.DCC28;
					speedCmd.Speed = 5;
					_system.Execute(speedCmd);

					functionCmd.Functions[0] = true;
					result = _system.Execute(functionCmd);
					if (!result!.OK) Console.WriteLine(((IResultError)result)!.ToString());

					speedCmd.Direction = DCCDirection.Stop;
					speedCmd.SpeedSteps = DCCProtocol.DCC28;
					speedCmd.Speed = 0;
					_system.Execute(speedCmd);
				}
			}
		}

		[TestMethod]
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

		[TestMethod]
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

		[TestMethod]
		public void SetSpeedTest() {
			if (_system != null && _system.Adapter != null) {
				if (_system.CreateCommand<ICmdLocoSetSpeed>() is ICmdLocoSetSpeed cmd) {
					cmd.Address = new DCCAddress(3, DCCAddressType.Short);
					cmd.Direction = DCCDirection.Forward;
					cmd.SpeedSteps = DCCProtocol.DCC28;
					cmd.Speed = 8;
					_system.Execute(cmd);
					Thread.Sleep(1500);

					cmd.Direction = DCCDirection.Forward;
					cmd.SpeedSteps = DCCProtocol.DCC28;
					cmd.Speed = 0;
					_system.Execute(cmd);
					Thread.Sleep(1500);

					cmd.Direction = DCCDirection.Reverse;
					cmd.SpeedSteps = DCCProtocol.DCC28;
					cmd.Speed = 8;
					_system.Execute(cmd);
					Thread.Sleep(1500);

					cmd.Direction = DCCDirection.Reverse;
					cmd.SpeedSteps = DCCProtocol.DCC28;
					cmd.Speed = 0;
					_system.Execute(cmd);
					Thread.Sleep(1500);
				}
			}
		}

		[TestMethod]
		public void SetStopTest() {
			if (_system != null && _system.Adapter != null) {
				if (_system.CreateCommand<ICmdLocoSetSpeed>() is ICmdLocoSetSpeed cmd) {
					cmd.Address = new DCCAddress(3, DCCAddressType.Short);
					cmd.Direction = DCCDirection.Forward;
					cmd.SpeedSteps = DCCProtocol.DCC28;
					cmd.Speed = 8;
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
}