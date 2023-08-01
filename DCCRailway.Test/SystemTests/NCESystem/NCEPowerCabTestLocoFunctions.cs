namespace DCCRailway.Test.SystemTests.NCESystem;
/*

    [TestClass]
    public class NCEPowerCabLocoFunctions {

        IAdapter _adapter;
        IDccSystem _system;

        [TestInitialize]
        public void Setup() {

            _adapter = new SerialAdapter("COM3", 19200);
            Assert.IsNotNull(_adapter, "Should have a Serial Adapter created");

            _system = SystemFactory.Create("NCE", "PowerCab", _adapter);
            Assert.IsNotNull(_system, "Should have an NCE PowerCab system created.");
            Assert.IsInstanceOfType(_system, typeof(DccSystems.NCE.NCEPowerCab), "Should be a NCE:NCEPowerCab System Created");

            Assert.IsNotNull(_system.Reference, "System Reference should not be null");
            Assert.AreEqual(_system.Reference?.Manufacturer, "NCE");

        }

        [TestCleanup]
        public void Cleanup() {
            _adapter.Disconnect();
        }


        [TestMethod]
        public void StopLoco() {

            ISetLocoSpeed? locoCmd = _system.CreateCommand<ISetLocoSpeed>() as ISetLocoSpeed;
            if (locoCmd == null) throw new UnsupportedCommandException("Could not create a ISetLocoSpeed object.");

            // Set the Train to move forward for 2 seconds and hen issue an emergency stop
            locoCmd.Speed = 12;
            locoCmd.Direction = LocoDirection.Forward;
            locoCmd.SpeedSteps = LocoSpeedSteps.DCC28;
            _system.ExecCommand(locoCmd);

            Thread.Sleep(3000);

            locoCmd.EmergencyStop = true;
            _system.ExecCommand(locoCmd);

            // Set the Train to move forward for 2 seconds and hen issue an emergency stop
            locoCmd.Speed = 10;
            locoCmd.Direction = LocoDirection.Reverse;
            locoCmd.SpeedSteps = LocoSpeedSteps.DCC28;
            locoCmd.EmergencyStop = false;
            _system.ExecCommand(locoCmd);

            Thread.Sleep(2000);

            locoCmd.EmergencyStop = true;
            _system.ExecCommand(locoCmd);

        }

        [TestMethod]
        public void SetFunctions() { }

        [TestMethod]
        public void MoveTrainForward() { }

        [TestMethod]
        public void MoveTrainBackwards() { }


        [TestMethod]
        public void CheckDummyStatus() {

            var systems = SystemFactory.SupportedSystems();

            // Create the Adapter and an instance of the System
            // ------------------------------------------------------------------------------------
            //var adapter = new SerialAdapter("cu.SLAB_USBtoUART",19200);

            // Setup some event management
            // --------------------------------------------
        }
    }
*/