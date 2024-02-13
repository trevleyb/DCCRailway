using System.Diagnostics;
using System.Reflection;
using DCCRailway.System.Adapters;
using DCCRailway.Utilities.Exceptions;

namespace DCCRailway.System.Controllers;

/// <summary>
/// The controller info class is used to store information about the controller. This is
/// dynamically read by scanning all assemblies in the current folder and determining
/// attributes and information about the controller.
/// </summary>
[DebuggerDisplay("Name: {Name}, Manufacturer: {Manufacturer}, Model: {Model}, Version: {Version}")]
public class ControllerInfo(ControllerAttribute attributes, string assemblyPath, Type assemblyType)
{
    private ControllerAttribute Attributes   { get; } = attributes;
    private Type                AssemblyType { get; } = assemblyType;
    private string              AssemblyPath { get; } = assemblyPath;

    public string Name         => Attributes?.Name          ?? "Unknown";
    public string Manufacturer => Attributes?.Manufacturer  ?? "Unknown";
    public string Model        => Attributes?.Model         ?? "Unknown";
    public string Version      => Attributes?.Version       ?? "Unknown";

    /// <summary>
    ///     Helper function to create an instance of a SYSTEM with an appropriate
    ///     adapter to connect to that controller. An error will be thrown if the
    ///     controller does not support the type of adapter being requested.
    /// </summary>
    /// <param name="adapter">An instance of an adapter to connect to</param>
    /// <returns>An instance of a DCCSystem to use to control</returns>
    /// <exception cref="ApplicationException"></exception>
    public IController Create(IAdapter adapter) {
        var controller = Create();
        controller.Adapter = adapter;
        return controller;
    }

    /// <summary>
    ///     Create an instance of a SYSTEM and return it.
    /// </summary>
    /// <returns>An instance of a controller</returns>
    /// <exception cref="ApplicationException">If it cannot create an instance dynamically</exception>
    public IController Create() {
        try {
            if (!File.Exists(AssemblyPath)) throw new SystemInstantiateException(Name, $"The Assembly '{AssemblyPath}' does not exist."); 
            if (AssemblyType is null) throw new SystemInstantiateException(Name, "Unable to determine the object type as the type is 'Undefined'.");

            var assembly = Assembly.LoadFrom(AssemblyPath);
            if (assembly is null) throw new SystemInstantiateException(Name, $"Unable to get the Assembly from the Path '{AssemblyPath}'.");
            if (Activator.CreateInstance(AssemblyType) is not IController instance) throw new SystemInstantiateException(Name, "Unable to instantiate an instance of the controller.");

            return instance;
        } catch (Exception ex) {
            throw new ApplicationException($"Unable to instantiate a new '{Name}' from {AssemblyPath}", ex);
        }
    }
    
    
}