using System.Text.Json;
using System.Text.Json.Serialization;
using DCCRailway.Common.Entities;

namespace DCCRailway.Client.Services;

public class LocomotiveService(HttpClient httpClient) {
    public async Task<List<Locomotive>?> GetLocomotivesAsync() {
        try {
            var options = new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition      = JsonIgnoreCondition.WhenWritingNull
            };

            var json        = await httpClient.GetStringAsync($"/api/Locomotives");
            var locomotives = JsonSerializer.Deserialize<List<Locomotive>>(json, options);
            return locomotives ?? [];
        } catch (Exception ex) {
            Console.WriteLine(ex.Message);
            return [];
        }
    }
}