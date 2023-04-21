using System;
using System.Linq;
using DCCRailway.Core.Attributes;
using DCCRailway.Core.Systems;
using DCCRailway.Core.Systems.Adapters;
using DCCRailway.Core.Systems.Attributes;
using DCCRailway.Core.Systems.Commands;

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
    
    public static AdapterAttribute Info(this IAdapter adapter) => GetAttribute<AdapterAttribute>(adapter.GetType())!;
    public static SystemAttribute  Info(this ISystem system) => GetAttribute<SystemAttribute>(system.GetType())!;
    public static CommandAttribute Info(this ICommand command) => GetAttribute<CommandAttribute>(command.GetType())!;

}