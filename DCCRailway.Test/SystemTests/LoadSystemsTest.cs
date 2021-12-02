using DCCRailway.Core.Systems;
using DCCRailway.Core.Systems.Adapters;
using DCCRailway.Systems.Virtual;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VirtualAdapter = DCCRailway.Systems.Virtual.VirtualAdapter;

namespace DCCRailway.Test {
	[TestClass]
	public class LoadSystemsTest {
		[TestMethod]
		public void LoadSystemsList() {
			var systems = SystemFactory.SupportedSystems();
			Assert.IsNotNull(systems, "Should have at least 1 system retuned from the GetListOfSystens call");
		}

		[TestMethod]
		public void InstantiateVirtual() {
			IAdapter? adapter = new VirtualAdapter();
			Assert.IsInstanceOfType(adapter, typeof(VirtualAdapter), "Should be a Virtual System Created");

			ISystem? system;

			system = SystemFactory.Create("Virtual", "Virtual", adapter);
			Assert.IsNotNull(system);
			Assert.IsInstanceOfType(system, typeof(VirtualSystem), "Should be a Virtual:Virtual System Created");

			system = SystemFactory.Create("Virtual", "Virtual", adapter);
			Assert.IsNotNull(system);
			Assert.IsInstanceOfType(system, typeof(VirtualSystem), "Should be a :Virtual System Created");
		}

		[TestMethod]
		public void CheckVirtualCommands() {
			var systems = SystemFactory.SupportedSystems();
			Assert.IsNotNull(systems, "Should have valid list of systems");

			var system = SystemFactory.Create("Virtual", "Virtual", new VirtualAdapter());
			Assert.IsNotNull(system, "Should have created a Virtual System");

			Console.WriteLine("---------------------------------------------------------------");

			var commandsSupported = system.SupportedCommands;
			Assert.IsNotNull(commandsSupported);
			Assert.AreEqual(commandsSupported?.Count, 2);

			if (commandsSupported != null) {
				foreach (var cmd in commandsSupported) {
					Console.WriteLine("Command==>" + cmd);
				}
			}
		}
	}
}