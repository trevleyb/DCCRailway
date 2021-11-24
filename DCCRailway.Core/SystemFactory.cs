using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using DCCRailway.Core.Adapters;

namespace DCCRailway.Core {
	public static class SystemFactory {
		private static List<SystemEntry> _supportedSystems;

		// <summary>
		/// Create by Assembly bypasses the search for a list of supported system and allows the
		/// creation of a system based on a Assembly File and Assemlbly type. For example, the file
		/// could be 'NCE.dll' and the type could be 'NCE.NCEPowerCab'
		/// </summary>
		/// <param name="assemblyBase">The base name (eg: 'NCE') of the assembly library (DCCRailway.System...dll)</param>
		/// <param name="systemName">The type name of the system to load (eg: NCEPowerCab)</param>
		/// <returns></returns>
		private static ISystem? LoadFromAssembly(string assemblyBase, string systemName) {
			var assemblyPath = "DCCRailway.Core." + assemblyBase + ".dll";
			var assemblyName = "DCCRailway.Core." + assemblyBase + "." + systemName;
			try {
				var assembly = Assembly.LoadFrom(assemblyPath);
				var type = assembly.GetType(assemblyName);
				return type != null ? Activator.CreateInstance(type) as ISystem : null;
			} catch (Exception ex) {
				throw new ApplicationException($"[SystemFactory] Unable to load ISystem: Path='{assemblyPath}' | Type='{assemblyName}'", ex);
			}
		}

		/// <summary>
		///     Create a System Interface by the Manufacturer and System name
		///     (ie: NCE,ProCab) and an adapter
		/// </summary>
		/// <param name="manufacturer">The name of the manufacturer</param>
		/// <param name="systemName">The name of the system to create</param>
		/// <param name="adapter">A reference to an adapter (ie: serial)</param>
		/// <returns>An ISystem instance</returns>
		/// <exception cref="ApplicationException"></exception>
		public static ISystem Create(string manufacturer, string systemName, IAdapter? adapter = null, string defaultPath = ".") {
			var system = Find(manufacturer, systemName, defaultPath);
			if (system != null) return system.Create(adapter);
			throw new ApplicationException("Invalid Manufacturer or System Name provided");
		}

		#region Helper functions to get a list of all supported systems
		/// <summary>
		///     The function returns a collection of supported systems by looking at all libraries in the current folder
		///     (or by override the path) and looking for classes that support the ISystem interface. Once you have obtained
		///     a SupportedSystem instance, you can use that instance to create a system and to make operations on that system.
		/// </summary>
		/// <param name="defaultPath"></param>
		/// <returns>List of systems that have been found</returns>
		public static List<SystemEntry> SupportedSystems(string defaultPath = ".", bool forceReload = false) {
			// Return previously loaded collection (for performance) if we have called this method before
			// if we force a reload then clean and reload the data
			// -----------------------------------------------------------------------------------------------------------
			if (_supportedSystems.Count > 0 && forceReload == false) return _supportedSystems;

			// Get a list of files in the current folder and then look at each one to see if it is a DCCSYstem assembly
			var pattern = @"(.*)DCCRailway\.Systems.(\D+)\.dll";
			if (!Directory.Exists(defaultPath)) throw new ApplicationException("[SystemFactory] Invalid Path provided for the Assembly Search");
			var fileEntries = Directory.GetFiles(defaultPath).Where(path => Regex.Match(path, pattern).Success);
			if (fileEntries == null || fileEntries.Any() == false) throw new ApplicationException("[SystemFactory] Could not find any System Assemblies 'DCCRailway.System.");

			// Process each file and load in its system information looking for IDCCSystem as an interface
			_supportedSystems = new List<SystemEntry>();
			foreach (var assemblyPath in fileEntries) {
				try {
					var assembly = Assembly.LoadFrom(assemblyPath);
					if (assembly == null) throw new ApplicationException($"Unable to load assembly: {assemblyPath}");
					Console.WriteLine($"Loading Assembly: {assemblyPath}");

					// Loop through each "CLASS" that supports the IDCCSystem Interface
					// ----------------------------------------------------------------------------------------------------------------
					foreach (var dccSystem in assembly.DefinedTypes.Where(t => t.ImplementedInterfaces.Contains(typeof(ISystem)))) // Get the relevant properties needed to report on what this system is via reflecting into the
						// class instance members. 
						// -------------------------------------------------------------------------------------------
					{
						try {
							var system = new SystemEntry(assemblyPath, dccSystem) {
								Manufacturer = dccSystem.DeclaredProperties.First(t => t.Name.Equals("Manufacturer")).GetValue(null, null) as string ?? "Unknown",
								SystemName = dccSystem.DeclaredProperties.First(t => t.Name.Equals("SystemName")).GetValue(null, null) as string ?? "Unknown"
							};
							_supportedSystems.Add(system);
						} catch (Exception ex) {
							Console.WriteLine("Unable to obtain the name of the Manufacturer or System from the Assembly.", ex);
						}
					}
				} catch (Exception ex) {
					throw new ApplicationException($"Could not load assembly: {assemblyPath}", ex);
				}
			}

			return _supportedSystems;
		}

		public static SystemEntry? Find(string manufacturer, string systemName, string defaultPath) {
			return SupportedSystems(defaultPath).Find(x => x.SystemName.Equals(systemName, StringComparison.OrdinalIgnoreCase) && x.Manufacturer.Equals(manufacturer, StringComparison.OrdinalIgnoreCase));
		}

		public static SystemEntry? Find(string systemName, string defaultPath) {
			return SupportedSystems(defaultPath).Find(x => x.SystemName.Equals(systemName, StringComparison.OrdinalIgnoreCase));
		}
		#endregion
	}
}