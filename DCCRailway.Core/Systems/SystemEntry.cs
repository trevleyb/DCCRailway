using System;
using System.Reflection;
using DCCRailway.Core.Exceptions;
using DCCRailway.Core.Systems.Adapters;

namespace DCCRailway.Core.Systems; 

/// <summary>
///     This is a holder class which contains information loaded form the file system
///     about each concrete system that is supported along. It then allows accessing what
///     commands are supported and what adapters are supported.
///     This also allows the creation of a system by calling the Create helper
///     method which takes an ADAPTER as a parameter
/// </summary>
public class SystemEntry {
    public SystemEntry(string assemblyPath, Type assemblyType, string manufacturer = "UNKNOWN", string systemName = "UNKNOWN") {
        AssemblyPath = assemblyPath;
        AssemblyType = assemblyType;
        Manufacturer = manufacturer;
        SystemName = systemName;
    }

    private Type AssemblyType { get; }
    private string AssemblyPath { get; }
    public string Manufacturer { get; set; }
    public string SystemName { get; set; }

    /// <summary>
    ///     Helper function to create an instance of a SYSTEM with an appropriate
    ///     adapter to connect to that system. An error will be thrown if the
    ///     system does not suport the type of adapter being requested.
    /// </summary>
    /// <param name="adapter">An instance of an adapter to connect to</param>
    /// <returns>An instance of a DCCSystem to use to control</returns>
    /// <exception cref="ApplicationException"></exception>
    public ISystem Create(IAdapter? adapter) {
        var system = Create();
        system.Adapter = adapter;
        return system;
    }

    /// <summary>
    ///     Create an instance of a SYSTEM and return it.
    /// </summary>
    /// <returns>An instance of a system</returns>
    /// <exception cref="ApplicationException">If it cannot create an instance dynamically</exception>
    private ISystem Create() {
        try {
            var assembly = Assembly.LoadFrom(AssemblyPath);
            if (assembly == null) throw new SystemInstantiateException(SystemName, $"Unable to get the Assembly from the Path '{AssemblyPath}'.");
            if (AssemblyType == null) throw new SystemInstantiateException(SystemName, "Unable to determine the object type as the type is 'Undefined'.");
            if (Activator.CreateInstance(AssemblyType) is not ISystem instance) throw new SystemInstantiateException(SystemName, "Unable to instantiate an instance of the system.");
            return instance;
        }
        catch (Exception ex) {
            throw new ApplicationException($"Unable to instantiate a new '{SystemName}' from {AssemblyPath}", ex);
        }
    }
}