using System.Reflection;
using System.Text.RegularExpressions;
using DCCRailway.Common.Utilities;
using DCCRailway.Station.Attributes;

namespace DCCRailway.Station.Controllers;

/// <summary>
/// The Controller Factory instantiates a list of available controllers and then
/// allows the caller to dynamically create a controller based on the name of the controller.
/// </summary>
public class ControllerFactory {
    private       Dictionary<string, ControllerManager>? _controllers           = new();
    private const string                              DefaultAssemblyPattern = @"(.*)DCCRailway.Station\.(\D+)\.dll";
    private const string                              DefaultPath            = ".";

    /// <summary>
    /// Get a list of all available controllers. If the current list is empty, then call the load function
    /// to read the current director and find all classes that are IController and then load them into the
    /// list to be returned.  
    /// </summary>
    public List<ControllerManager> Controllers {
        get {
            if (_controllers == null || _controllers.Any() == false) LoadControllers();
            return _controllers?.Values.ToList() ?? [];
        }
    }

    /// <summary>
    /// Create a New Controller directly from the Factory without any further details (ignore the Controller info)
    /// </summary>
    /// <param name="name">The Name of the Controller to create</param>
    /// <returns>A new Controller</returns>
    /// <exception cref="ApplicationException">If the name is not valod</exception>
    public IController CreateController(string name) {
        var controller = Find(name);
        if (controller is null) throw new ApplicationException($"Controller {name} not found");
        return controller.Create();
    }

    /// <summary>
    ///     The function returns a collection of supported systems by looking at all libraries in the current folder
    ///     (or by override the path) and looking for classes that support the IController interface. Once you have obtained
    ///     a SupportedSystem instance, you can use that instance to create a controller and to make operations on that controller.
    /// </summary>
    /// <param name="defaultPath"></param>
    /// <returns>List of systems that have been found</returns>
    private void LoadControllers() {
        _controllers = new Dictionary<string, ControllerManager>();

        var path    = DefaultPath;
        var pattern = DefaultAssemblyPattern;

        // Get a list of files in the current folder and then look at each one to see if it is a DCCSystem assembly
        // ---------------------------------------------------------------------------------------------------------
        if (!Directory.Exists(path)) throw new ApplicationException("[Controllers] Invalid Path provided for the Controller Assembly Search");
        var assemblies = Directory.GetFiles(DefaultPath).Where(directory => Regex.Match(directory, pattern).Success).ToList();
        if (assemblies == null || assemblies.Any() == false) throw new ApplicationException($"[Controllers] Could not find any Controller Assemblies '{path} => {pattern}'");

        // Process each file and load in its controller information looking for IDCCSystem as an interface
        // -------------------------------------------------------------------------------------------
        foreach (var assemblyPath in assemblies) {
            try {
                var assembly = Assembly.LoadFrom(assemblyPath);

                if (assembly is null) throw new ApplicationException($"Unable to load assembly: {assemblyPath}");
                Logger.Log.Debug($"ASSEMBLY: Loading Assembly: {assemblyPath}");

                foreach (var controller in assembly.DefinedTypes.Where(t => t.ImplementedInterfaces.Contains(typeof(IController)))) {
                    // Get the relevant properties needed to report on what this controller is via reflecting into the
                    // class instance members. 
                    // -------------------------------------------------------------------------------------------
                    try {
                        var systemAttr = AttributeExtractor.GetAttribute<ControllerAttribute>(controller); //(SystemNameAttribute) Attribute.GetCustomAttribute(dccSystem, typeof(SystemNameAttribute))!;
                        if (systemAttr is not null) {
                            if (_controllers.ContainsKey(systemAttr.Name))
                                throw new ApplicationException($"Duplicate Controller Name found: {systemAttr.Name}");
                            _controllers.Add(systemAttr.Name, new ControllerManager(systemAttr, assemblyPath, controller));
                        }
                    }
                    catch (Exception ex) {
                        Logger.Log.Debug("ASSEMBLY: Unable to obtain the name of the Manufacturer or Controller from the Assembly.", ex);
                    }
                }
            }
            catch (Exception ex) {
                throw new ApplicationException($"Could not load assembly: {assemblyPath}", ex);
            }
        }
    }

    public List<ControllerManager> FindByManufacturer(string manufacturer) => _controllers?.Where(key => key.Value.Manufacturer.Equals(manufacturer, StringComparison.InvariantCultureIgnoreCase)).Select(key => key.Value).ToList() ?? [];
    public ControllerManager? this[string                    name] => Find(name);
    public ControllerManager? Find(string                    name) => Controllers?.FirstOrDefault(key => key.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)) ?? null;
}