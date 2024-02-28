using System.Reflection;
using DCCRailway.DCCController.Adapters;
using DCCRailway.DCCController.Commands;
using DCCRailway.DCCController.Controllers;

namespace DCCRailway.Utilities;

public static class AttributeExtractor {
    public static T? GetAttribute<T>(ICustomAttributeProvider type) where T : class {
        try {
            var attrs = type.GetCustomAttributes(typeof(T), true);
            var attr  = type.GetCustomAttributes(typeof(T), true).FirstOrDefault() as T;
            return attr;
        } catch {
            return default(T?) ?? null;
        }
    }

    public static AdapterAttribute AttributeInfo(this IAdapter adapter) => GetAttribute<AdapterAttribute>(adapter.GetType())!;

    public static ControllerAttribute AttributeInfo(this IController controller) => GetAttribute<ControllerAttribute>(controller.GetType())!;

    public static CommandAttribute AttributeInfo(this ICommand command) => GetAttribute<CommandAttribute>(command.GetType())!;
}