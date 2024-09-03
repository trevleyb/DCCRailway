using System.Reflection;
using System.Text.RegularExpressions;
using DCCRailway.Controller.Attributes;
using Serilog;

namespace DCCRailway.Controller.Controllers;

/// <summary>
///     The CommandStation Factory instantiates a list of available controllers and then
///     allows the caller to dynamically create a controller based on the name of the controller.
/// </summary>
public class CommandStationFactory(ILogger logger) {
    private const string                                     DefaultAssemblyPattern = @"(.*)DCCRailway.Controller\.(\D+)\.dll";
    private const string                                     DefaultPath            = ".";
    private       Dictionary<string, CommandStationManager>? _controllers           = new();

    /// <summary>
    ///     Get a list of all available controllers. If the current list is empty, then call the load function
    ///     to read the current director and find all classes that are ICommandStation and then load them into the
    ///     list to be returned.
    /// </summary>
    public List<CommandStationManager> Controllers {
        get {
            if (_controllers == null || _controllers.Any() == false) LoadControllers();
            return _controllers?.Values.ToList() ?? [];
        }
    }

    public CommandStationManager? this[string name] => Find(name);

    /// <summary>
    ///     Create a New CommandStation directly from the Factory without any further details (ignore the CommandStation info)
    /// </summary>
    /// <param name="name">The Name of the CommandStation to create</param>
    /// <returns>A new CommandStation</returns>
    /// <exception cref="ApplicationException">If the name is not valod</exception>
    public ICommandStation CreateController(string name) {
        var controller = Find(name);
        if (controller is null) throw new ApplicationException($"CommandStation {name} not found");
        return controller.Create();
    }

    /// <summary>
    ///     The function returns a collection of supported systems by looking at all libraries in the current folder
    ///     (or by override the path) and looking for classes that support the ICommandStation interface. Once you have
    ///     obtained
    ///     a SupportedSystem instance, you can use that instance to create a controller and to make operations on that
    ///     controller.
    /// </summary>
    /// <param name="defaultPath"></param>
    /// <returns>List of systems that have been found</returns>
    private void LoadControllers() {
        _controllers = new Dictionary<string, CommandStationManager>();

        var path    = DefaultPath;
        var pattern = DefaultAssemblyPattern;

        // Get a list of files in the current folder and then look at each one to see if it is a DCCSystem assembly
        // ---------------------------------------------------------------------------------------------------------
        if (!Directory.Exists(path)) {
            throw new ApplicationException("[Controllers] Invalid Path provided for the CommandStation Assembly Search");
        }

        var executingAssembly  = Assembly.GetExecutingAssembly();
        var executingDirectory = Path.GetDirectoryName(executingAssembly.Location) ?? path;
        var assemblies         = Directory.GetFiles(executingDirectory).Where(directory => Regex.Match(directory, pattern).Success).ToList();

        if (assemblies == null || assemblies.Any() == false) {
            throw new ApplicationException($"[Controllers] Could not find any CommandStation Assemblies '{path} => {pattern}'");
        }

        // Process each file and load in its controller information looking for IDCCSystem as an interface
        // -------------------------------------------------------------------------------------------
        foreach (var assemblyPath in assemblies) {
            try {
                var assembly = Assembly.LoadFrom(assemblyPath);

                if (assembly is null) throw new ApplicationException($"Unable to load assembly: {assemblyPath}");
                logger.Debug($"ASSEMBLY: Loading Assembly: {assemblyPath}");

                foreach (var controller in assembly.DefinedTypes.Where(t => t.ImplementedInterfaces.Contains(typeof(ICommandStation))))

                    // Get the relevant properties needed to report on what this controller is via reflecting into the
                    // class instance members. 
                    // -------------------------------------------------------------------------------------------
                {
                    try {
                        var systemAttr = AttributeExtractor.GetAttribute<ControllerAttribute>(controller); //(SystemNameAttribute) Attribute.GetCustomAttribute(dccSystem, typeof(SystemNameAttribute))!;

                        if (systemAttr is not null) {
                            if (_controllers.ContainsKey(systemAttr.Name)) {
                                throw new ApplicationException($"Duplicate CommandStation Name found: {systemAttr.Name}");
                            }

                            _controllers.Add(systemAttr.Name, new CommandStationManager(logger, systemAttr, assemblyPath, controller));
                        }
                    } catch (Exception ex) {
                        logger.Debug("ASSEMBLY: Unable to obtain the name of the Manufacturer or CommandStation from the Assembly.", ex);
                    }
                }
            } catch (Exception ex) {
                throw new ApplicationException($"Could not load assembly: {assemblyPath}", ex);
            }
        }
    }

    public List<CommandStationManager> FindByManufacturer(string manufacturer) {
        return _controllers?.Where(key => key.Value.Manufacturer.Equals(manufacturer, StringComparison.InvariantCultureIgnoreCase)).Select(key => key.Value).ToList() ?? [];
    }

    public CommandStationManager? Find(string name) {
        return Controllers?.FirstOrDefault(key => key.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)) ?? null;
    }
}