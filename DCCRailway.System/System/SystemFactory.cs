using System.Reflection;
using System.Text.RegularExpressions;
using DCCRailway.System.Adapters;
using DCCRailway.System.Attributes;
using DCCRailway.System.Utilities;

namespace DCCRailway.System;

public static class xxxSystemFactory {
    // <summary>
    /// Create by Assembly bypasses the search for a list of supported controller and allows the
    /// creation of a controller based on a Assembly File and Assemlbly type. For example, the file
    /// could be 'NCE.dll' and the type could be 'NCE.NCEPowerCab'
    /// </summary>
    /// <param name="assemblyBase">The base name (eg: 'NCE') of the assembly library (DCCRailway.Controller...dll)</param>
    /// <param name="systemName">The type name of the controller to load (eg: NCEPowerCab)</param>
    /// <returns></returns>
    /*
    private static IController? LoadFromAssembly(string assemblyBase, string systemName) {
        var assemblyPath = "DCCRailway.Controller." + assemblyBase + ".dll";
        var assemblyName = "DCCRailway.Controller." + assemblyBase + "." + systemName;
        try {
            var assembly = Assembly.LoadFrom(assemblyPath);
            var type = assembly.GetType(assemblyName);
            return type != null ? Activator.CreateInstance(type) as IController : null;
        } catch (Exception ex) {
            throw new ApplicationException($"[SystemFactory] Unable to load IController: Path='{assemblyPath}' | Types='{assemblyName}'", ex);
        }
    }
*/

    /// <summary>
    ///     Create a Controller Interface by the Manufacturer and Controller name
    ///     (ie: NCE,ProCab) and an adapter
    /// </summary>
    /// <param name="manufacturer">The name of the manufacturer</param>
    /// <param name="systemName">The name of the controller to create</param>
    /// <param name="adapter">A reference to an adapter (ie: serial)</param>
    /// <returns>An IController instance</returns>
    /// <exception cref="ApplicationException"></exception>
    public static IController Create(string manufacturer, string systemName, IAdapter? adapter = null, string defaultPath = ".") {
        var system = Find(systemName, defaultPath);

        if (system != null) return system.Create(adapter);

        throw new ApplicationException("Invalid Manufacturer or Controller Name provided");
    }

    public static IController Create(string systemName, IAdapter? adapter = null, string defaultPath = ".") {
        var system = Find(systemName, defaultPath);

        if (system != null) return system.Create(adapter);

        throw new ApplicationException("Invalid Manufacturer or Controller Name provided");
    }

    /// <summary>
    ///     The function returns a collection of supported systems by looking at all libraries in the current folder
    ///     (or by override the path) and looking for classes that support the IController interface. Once you have obtained
    ///     a SupportedSystem instance, you can use that instance to create a controller and to make operations on that controller.
    /// </summary>
    /// <param name="defaultPath"></param>
    /// <returns>List of systems that have been found</returns>
    public static List<xxxSystemEntry> SupportedSystems(string defaultPath = ".") {
        // Get a list of files in the current folder and then look at each one to see if it is a DCCSystem assembly
        // ---------------------------------------------------------------------------------------------------------
        const string pattern = @"(.*)DCCRailway\.Controller.(\D+)\.dll";

        if (!Directory.Exists(defaultPath)) throw new ApplicationException("[SystemFactory] Invalid Path provided for the Assembly Search");
        var fileEntries = Directory.GetFiles(defaultPath).Where(path => Regex.Match(path, pattern).Success).ToList();

        if (fileEntries == null || fileEntries.Any() == false) throw new ApplicationException("[SystemFactory] Could not find any Controller Assemblies 'DCCRailway.Controller.");

        // Process each file and load in its controller information looking for IDCCSystem as an interface
        // -------------------------------------------------------------------------------------------
        var supportedSystems = new List<xxxSystemEntry>();

        foreach (var assemblyPath in fileEntries) {
            try {
                var assembly = Assembly.LoadFrom(assemblyPath);

                if (assembly == null) throw new ApplicationException($"Unable to load assembly: {assemblyPath}");
                Logger.Log.Debug($"ASSEMBLY: Loading Assembly: {assemblyPath}");

                foreach (var dccSystem in assembly.DefinedTypes.Where(t => t.ImplementedInterfaces.Contains(typeof(IController))))

                    // Get the relevant properties needed to report on what this controller is via reflecting into the
                    // class instance members. 
                    // -------------------------------------------------------------------------------------------
                {
                    try {
                        var systemAttr = AttributeExtractor.GetAttribute<ControllerAttribute>(dccSystem); //(SystemNameAttribute) Attribute.GetCustomAttribute(dccSystem, typeof(SystemNameAttribute))!;

                        if (systemAttr != null) {
                            supportedSystems.Add(new xxxSystemEntry(assemblyPath, dccSystem, systemAttr));
                        }
                    } catch (Exception ex) {
                        Logger.Log.Debug("ASSEMBLY: Unable to obtain the name of the Manufacturer or Controller from the Assembly.", ex);
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

    private static xxxSystemEntry? Find(string name, string defaultPath) => SupportedSystems(defaultPath).Find(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
}


/* IS THIS A BETTER WAY?

var assemblyName = new AssemblyName("DynamicAssembly");
   var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
   var moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");
   var typeBuilder = moduleBuilder.DefineType("MyDynamicType", TypeAttributes.Public);
   var dynamicType = typeBuilder.CreateType();
   // Now you have a brand new type to play with!



[Conditional("DEBUG")]
   public void LogDebugInfo(string message)
   {
       Console.WriteLine($"Debug: {message}");
   }
   
   public void ProcessData()
   {
       LogDebugInfo("Processing data..."); // This call only happens in DEBUG mode
   }



*/