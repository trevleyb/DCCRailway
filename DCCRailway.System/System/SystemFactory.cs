﻿using System.Reflection;
using System.Text.RegularExpressions;
using DCCRailway.System.Adapters;
using DCCRailway.System.Attributes;
using DCCRailway.System.Utilities;

namespace DCCRailway.System;

public static class SystemFactory {
    // <summary>
    /// Create by Assembly bypasses the search for a list of supported system and allows the
    /// creation of a system based on a Assembly File and Assemlbly type. For example, the file
    /// could be 'NCE.dll' and the type could be 'NCE.NCEPowerCab'
    /// </summary>
    /// <param name="assemblyBase">The base name (eg: 'NCE') of the assembly library (DCCRailway.System...dll)</param>
    /// <param name="systemName">The type name of the system to load (eg: NCEPowerCab)</param>
    /// <returns></returns>
    /*
    private static ISystem? LoadFromAssembly(string assemblyBase, string systemName) {
        var assemblyPath = "DCCRailway.System." + assemblyBase + ".dll";
        var assemblyName = "DCCRailway.System." + assemblyBase + "." + systemName;
        try {
            var assembly = Assembly.LoadFrom(assemblyPath);
            var type = assembly.GetType(assemblyName);
            return type != null ? Activator.CreateInstance(type) as ISystem : null;
        } catch (Exception ex) {
            throw new ApplicationException($"[SystemFactory] Unable to load ISystem: Path='{assemblyPath}' | Types='{assemblyName}'", ex);
        }
    }
*/

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
        var system = Find(systemName, defaultPath);

        if (system != null) return system.Create(adapter);

        throw new ApplicationException("Invalid Manufacturer or System Name provided");
    }

    public static ISystem Create(string systemName, IAdapter? adapter = null, string defaultPath = ".") {
        var system = Find(systemName, defaultPath);

        if (system != null) return system.Create(adapter);

        throw new ApplicationException("Invalid Manufacturer or System Name provided");
    }

    /// <summary>
    ///     The function returns a collection of supported systems by looking at all libraries in the current folder
    ///     (or by override the path) and looking for classes that support the ISystem interface. Once you have obtained
    ///     a SupportedSystem instance, you can use that instance to create a system and to make operations on that system.
    /// </summary>
    /// <param name="defaultPath"></param>
    /// <returns>List of systems that have been found</returns>
    public static List<SystemEntry> SupportedSystems(string defaultPath = ".") {
        // Get a list of files in the current folder and then look at each one to see if it is a DCCSystem assembly
        // ---------------------------------------------------------------------------------------------------------
        const string pattern = @"(.*)DCCRailway\.System.(\D+)\.dll";

        if (!Directory.Exists(defaultPath)) throw new ApplicationException("[SystemFactory] Invalid Path provided for the Assembly Search");
        var fileEntries = Directory.GetFiles(defaultPath).Where(path => Regex.Match(path, pattern).Success).ToList();

        if (fileEntries == null || fileEntries.Any() == false) throw new ApplicationException("[SystemFactory] Could not find any System Assemblies 'DCCRailway.System.");

        // Process each file and load in its system information looking for IDCCSystem as an interface
        // -------------------------------------------------------------------------------------------
        var supportedSystems = new List<SystemEntry>();

        foreach (var assemblyPath in fileEntries) {
            try {
                var assembly = Assembly.LoadFrom(assemblyPath);

                if (assembly == null) throw new ApplicationException($"Unable to load assembly: {assemblyPath}");
                Logger.Log.Debug($"ASSEMBLY: Loading Assembly: {assemblyPath}");

                foreach (var dccSystem in assembly.DefinedTypes.Where(t => t.ImplementedInterfaces.Contains(typeof(ISystem))))

                    // Get the relevant properties needed to report on what this system is via reflecting into the
                    // class instance members. 
                    // -------------------------------------------------------------------------------------------
                {
                    try {
                        var systemAttr = AttributeExtractor.GetAttribute<SystemAttribute>(dccSystem); //(SystemNameAttribute) Attribute.GetCustomAttribute(dccSystem, typeof(SystemNameAttribute))!;

                        if (systemAttr != null) {
                            supportedSystems.Add(new SystemEntry(assemblyPath, dccSystem, systemAttr));
                        }
                    } catch (Exception ex) {
                        Logger.Log.Debug("ASSEMBLY: Unable to obtain the name of the Manufacturer or System from the Assembly.", ex);
                    }
                }
            } catch (Exception ex) {
                throw new ApplicationException($"Could not load assembly: {assemblyPath}", ex);
            }
        }

        return supportedSystems;
    }

    //private static SystemEntry? Find(string name, string manufacturer, string defaultPath) {
    //    return SupportedSystems(defaultPath).Find(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && x.Manufacturer.Equals(manufacturer, StringComparison.OrdinalIgnoreCase));
    //}

    private static SystemEntry? Find(string name, string defaultPath) => SupportedSystems(defaultPath).Find(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
}