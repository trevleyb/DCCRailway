using System.Text;
using DCCRailway.Common.State;
using DCCRailway.Layout.Layout.LayoutSDK;
using System.Text.Json;
using DCCRailway.Common.Helpers;
using Newtonsoft.Json;

namespace DCCRailway.Layout.StateManager.StateSDK;

public class States(string serviceUrl) {
    private readonly HttpClient _httpClient = new() { BaseAddress = new Uri(serviceUrl) };

    public List<StateObject>? GetAll() {
        return GetAllAsync().GetAwaiter().GetResult();
    }

    public async Task<List<StateObject>?> GetAllAsync() {
        var response = await _httpClient.GetAsync($"/states");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<StateObject>>(content);
    }

    public StateObject? Get(string id) {
        return GetAsync(id).GetAwaiter().GetResult();
    }

    public async Task<StateObject?> GetAsync(string id) {
        var response = await _httpClient.GetAsync($"/states/{id}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<StateObject>(content);
    }

    public StateObject Save(string id, StateObject state) {
        return SaveAsync(id, state).GetAwaiter().GetResult();
    }

    public StateObject Save(StateObject state) {
        return SaveAsync(state.Id, state).GetAwaiter().GetResult();
    }

    public async Task<StateObject> SaveAsync(string id, StateObject state) {
        var content = new StringContent(JsonConvert.SerializeObject(state), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"/states/{id}", content);
        if (response.IsSuccessStatusCode) {
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<StateObject>(result) ?? throw new Exception("Failed to add Entity.");
        }
        throw new Exception($"Failed to add entity: {response.ReasonPhrase}");
    }

    //public StateObject Update(string id, StateObject state) {
    //    return UpdateAsync(id,state).GetAwaiter().GetResult();
    //}

    //public async Task<StateObject> UpdateAsync(string id, StateObject state) {
    //    var content = new StringContent(JsonConvert.SerializeObject(state), Encoding.UTF8, "application/json");
    //    var response = await _httpClient.PutAsync($"/states/{id}", content);
    //    if (response.IsSuccessStatusCode) {
    //        var result = await response.Content.ReadAsStringAsync();
    //        return JsonConvert.DeserializeObject<StateObject>(result) ?? throw new Exception("Failed to add Entity.");
    //    }
    //    throw new Exception($"Failed to add entity: {response.ReasonPhrase}");
    //}

    public void Delete(string id) {
        DeleteAsync(id).GetAwaiter().GetResult();
    }

    public void Delete(StateObject state) {
        DeleteAsync(state.Id).GetAwaiter().GetResult();
    }

    public async Task DeleteAsync(string id) {
        var response = await _httpClient.DeleteAsync($"/states/{id}");
        if (response.IsSuccessStatusCode) {
            return;
        }
        throw new Exception($"Failed to delete entity: {response.ReasonPhrase}");
    }
}