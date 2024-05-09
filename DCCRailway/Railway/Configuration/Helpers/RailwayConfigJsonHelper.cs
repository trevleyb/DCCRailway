using Common.Logging;
using DCCRailway.Common.Helpers;

namespace DCCRailway.Railway.Configuration.Helpers;

public abstract class RailwayConfigJsonHelper {
    protected static IRailwayManager LoadConfigFromFile(string name) {
        try {
            var configuration = JsonSerializerHelper<RailwayManager>.LoadFile(name);
            if (configuration is not null) {
                configuration.Filename = name;
                return configuration as IRailwayManager;
            }
        } catch (Exception ex) {
            throw new ConfigurationException($"Unable to load the configuration file '{name}' due to '{ex.Message}'");
        }
        throw new ConfigurationException($"Unable to load the configuration file.");
    }

    protected void SaveConfigToFile(RailwayManager configuration, string name) {
        try {
            JsonSerializerHelper<RailwayManager>.SaveFile(configuration, name);
            configuration.Filename = name;
        } catch (Exception ex) {
            throw new ConfigurationException($"Unable to save the configuration file '{name}' due to '{ex.Message}'");
        }
    }
}