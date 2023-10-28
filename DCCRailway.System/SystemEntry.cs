using System.Reflection;
using DCCRailway.System.Adapters;
using DCCRailway.System.Attributes;
using DCCRailway.System.Exceptions;

namespace DCCRailway.System;

/// <summary>
///     This is a holder class which contains information loaded form the file system
///     about each concrete system that is supported along. It then allows accessing what
///     commands are supported and what adapters are supported.
///     This also allows the creation of a system by calling the Create helper
///     method which takes an ADAPTER as a parameter
/// </summary>
public class SystemEntry {
    public SystemEntry(string assemblyPath, Type assemblyType, SystemAttribute? attributes = null) {
        AssemblyPath = assemblyPath;
        AssemblyType = assemblyType;
        Attributes   = attributes ?? new SystemAttribute("Unknown");
    }

    private SystemAttribute Attributes   { get; }
    private Type            AssemblyType { get; }
    private string          AssemblyPath { get; }

    public string Name         => Attributes.Name;
    public string Manufacturer => Attributes.Manufacturer;
    public string Model        => Attributes.Model;
    public string version      => Attributes.Version;

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

            if (assembly == null) throw new SystemInstantiateException(Name, $"Unable to get the Assembly from the Path '{AssemblyPath}'.");
            if (AssemblyType == null) throw new SystemInstantiateException(Name, "Unable to determine the object type as the type is 'Undefined'.");
            if (Activator.CreateInstance(AssemblyType) is not ISystem instance) throw new SystemInstantiateException(Name, "Unable to instantiate an instance of the system.");

            return instance;
        } catch (Exception ex) {
            throw new ApplicationException($"Unable to instantiate a new '{Name}' from {AssemblyPath}", ex);
        }
    }
}