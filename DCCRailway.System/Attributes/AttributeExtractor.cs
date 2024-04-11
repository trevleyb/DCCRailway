using System.Reflection;
using DCCRailway.System.Adapters;
using DCCRailway.System.Commands;
using DCCRailway.System.Controllers;

namespace DCCRailway.System.Attributes
{
    public static class AttributeExtractor
    {
        public static T? GetAttribute<T>(ICustomAttributeProvider provider) where T : class
        {
            var attribute = provider.GetCustomAttributes(typeof(T), true).FirstOrDefault() as T;
            return HandleException<T>(attribute);
        }

        private static T? HandleException<T>(object? attr) where T : class
        {
            try {
                return attr as T;
            }
            catch {
                return default(T?) ?? null;
            }
        }

        public static AdapterAttribute    AttributeInfo(this IAdapter    adapter)    => GetAttribute<AdapterAttribute>(adapter.GetType()) ?? new AdapterAttribute("Unknown",AdapterType.Unknown);
        public static ControllerAttribute AttributeInfo(this IController controller) => GetAttribute<ControllerAttribute>(controller.GetType()) ?? new ControllerAttribute("Unknown");
        public static CommandAttribute    AttributeInfo(this ICommand    command)    => GetAttribute<CommandAttribute>(command.GetType()) ?? new CommandAttribute("Unknown","Unknown","Unknown", new[] { "!*" }, new[] { "!*" });
    }
}