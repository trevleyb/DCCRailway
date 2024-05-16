using System.Text.Json;
using System.Text.Json.Serialization;

namespace DCCRailway.Common.Helpers;

public static class JsonSerializerHelper {
    public static JsonSerializerOptions Options => new() {
        WriteIndented          = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

}