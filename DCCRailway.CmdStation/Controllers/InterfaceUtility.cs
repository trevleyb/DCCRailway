using System.Reflection;
using DCCRailway.CmdStation.Commands;

namespace DCCRailway.CmdStation.Controllers;

public static class InterfaceUtility {
    public static string? FindImplmentationInterface(IEnumerable<TypeInfo> definedTypes, string searchtype) {
        foreach (var definedType in definedTypes) {
            foreach (var implementedInterface in definedType.ImplementedInterfaces) {
                if (implementedInterface.Name.Equals(SplitInterfaceName(searchtype))) return definedType.FullName;
            }
        }
        return null;
    }

    public static string? FindImplementsInterface(TypeInfo definedType, string searchInterface) {
        foreach (var interfaceType in definedType.ImplementedInterfaces) {
            var foundName = SplitInterfaceName(interfaceType.FullName);
            if (foundName != null && foundName == searchInterface) return foundName;
        }
        return null;
    }

    public static List<string>? FindInterfaces(TypeInfo definedType, string searchInterface) {
        var isICommand      = false;
        var foundInterfaces = new List<string>();

        foreach (var interfaceType in definedType.ImplementedInterfaces) {
            var foundName = SplitInterfaceName(interfaceType.FullName);

            if (foundName != null) {
                if (foundName == searchInterface) {
                    isICommand = true;
                }
                else {
                    if (!string.IsNullOrEmpty(interfaceType.FullName)) foundInterfaces.Add(interfaceType.FullName);
                }
            }
        }
        return isICommand ? foundInterfaces : null;
    }

    public static string? SplitInterfaceName(string? fullname) {
        if (!string.IsNullOrEmpty(fullname)) {
            var split = fullname.Split(".");
            return split.Length > 0 ? split[^1] : null;
        }
        return null;
    }

    public static string? FindImplmentationInterface<T>(IEnumerable<TypeInfo> definedTypes) where T : ICommand => FindImplmentationInterface(definedTypes, typeof(T).ToString());
    public static bool ImplementsInterface(TypeInfo definedType, string searchInterface) => FindImplementsInterface(definedType, searchInterface) != null;

}