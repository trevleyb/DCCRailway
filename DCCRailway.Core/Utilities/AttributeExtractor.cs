using System;
using System.Linq;

namespace DCCRailway.Core.Utilities;

public static class AttributeExtractor {
    public static T? GetAttribute<T>(Type type) where T : class {
        try {
            var attr = type.GetCustomAttributes(typeof(T), true).FirstOrDefault() as T;

            return attr;
        }
        catch {
            return default(T?) ?? null;
        }
    }

    public static AdapterAttribute Info(this IAdapter adapter) {
        return GetAttribute<AdapterAttribute>(adapter.GetType())!;
    }

    public static SystemAttribute Info(this ISystem system) {
        return GetAttribute<SystemAttribute>(system.GetType())!;
    }

    public static CommandAttribute Info(this ICommand command) {
        return GetAttribute<CommandAttribute>(command.GetType())!;
    }
}