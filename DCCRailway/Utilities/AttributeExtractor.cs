using System.Reflection;
using DCCRailway.System.Layout.Adapters;
using DCCRailway.System.Layout.Commands;
using DCCRailway.System.Layout.Controllers;

namespace DCCRailway.System.Utilities;

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

    public static AdapterAttribute Info(this IAdapter adapter) => GetAttribute<AdapterAttribute>(adapter.GetType())!;

    public static ControllerAttribute Info(this IController controller) => GetAttribute<ControllerAttribute>(controller.GetType())!;

    public static CommandAttribute Info(this ICommand command) => GetAttribute<CommandAttribute>(command.GetType())!;
}