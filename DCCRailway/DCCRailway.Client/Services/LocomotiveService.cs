using System.Text.Json;
using DCCRailway.Common.Entities;

namespace DCCRailway.Client.Services;

public class LocomotiveService(HttpClient httpClient) {
    public async Task<List<Locomotive>?> GetLocomotivesAsync() {
        try {
            var json        = await httpClient.GetStringAsync($"/api/Locomotives");
            var locomotives = JsonSerializer.Deserialize<List<Locomotive>>(json);
            return locomotives ?? [];
        } catch (Exception ex) {
            Console.WriteLine(ex.Message);
            return [];
        }
    }
}