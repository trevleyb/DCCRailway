using Common.Logging;
using DCCRailway.Common.Helpers;

namespace DCCRailway.Layout.Configuration.Helpers;

public abstract class RailwayConfigJsonHelper<T> where T : IRailwayConfig
{
    public static T Load(string? name = null) {
        try {
            var loadname = name ?? RailwayConfig.DefaultConfigFilename;
            var configuration = JsonSerializerHelper<T>.LoadFile(loadname);
            if (configuration is not null) {
                configuration.Filename = loadname;
                return configuration;
            }
        } catch (Exception ex) {
            throw new ConfigurationException($"Unable to load the configuration file '{name}' due to '{ex.Message}'");
        }
        throw new ConfigurationException($"Unable to load the configuration file.");
    }

    public static T Save(T configuration, string? name = null) {
        try {
            var savename = name ?? RailwayConfig.DefaultConfigFilename;
            JsonSerializerHelper<T>.SaveFile(configuration, savename);
            configuration.Filename = savename;
            return configuration;
        } catch (Exception ex) {
            throw new ConfigurationException($"Unable to save the configuration file '{name}' due to '{ex.Message}'");
        }
    }
}