using System.Text;
using DCCRailway.Common.Helpers;
using Newtonsoft.Json;

namespace DCCRailway.Layout.LayoutSDK;

public abstract class Entity<TEntity> : IEntity<TEntity> {
    private readonly string     _entityType;
    private readonly HttpClient _httpClient;

    public Entity(string serviceUrl, string entityType) {
        _httpClient = new HttpClient { BaseAddress = new Uri(serviceUrl) };
        _entityType = entityType;
    }

    public TEntity? GetById(string id) {
        return GetByIdAsync(id).GetAwaiter().GetResult();
    }

    public async Task<TEntity?> GetByIdAsync(string id) {
        var response = await _httpClient.GetAsync($"/{_entityType}/{id}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TEntity>(content);
    }

    public TEntity? GetByName(string id) {
        return GetByNameAsync(id).GetAwaiter().GetResult();
    }

    public async Task<TEntity?> GetByNameAsync(string name) {
        var response = await _httpClient.GetAsync($"/{_entityType}?name={name}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TEntity>(content);
    }

    public IEnumerable<TEntity> GetAll() {
        return GetAllAsync().GetListFromAsyncEnumerable().GetAwaiter().GetResult();
    }

    public async IAsyncEnumerable<TEntity> GetAllAsync() {
        var response = await _httpClient.GetAsync($"/{_entityType}");
        response.EnsureSuccessStatusCode();
        var content  = await response.Content.ReadAsStringAsync();
        var entities = JsonConvert.DeserializeObject<List<TEntity>>(content) ?? [];
        foreach (var entity in entities) yield return await Task.Run(() => entity);
    }

    public TEntity? Find(Func<TEntity, bool> predicate) {
        return FindAsync(predicate).GetAwaiter().GetResult();
    }

    public async Task<TEntity?> FindAsync(Func<TEntity, bool> predicate) {
        var response = await _httpClient.GetAsync($"/{_entityType}");
        response.EnsureSuccessStatusCode();
        var content  = await response.Content.ReadAsStringAsync();
        var entities = JsonConvert.DeserializeObject<List<TEntity>>(content) ?? [];
        return entities.FirstOrDefault(predicate) ?? default(TEntity?);
    }

    public IEnumerable<TEntity> GetAll(Func<TEntity, bool> predicate) {
        return GetAllAsync(predicate).GetListFromAsyncEnumerable().GetAwaiter().GetResult();
    }

    public async IAsyncEnumerable<TEntity> GetAllAsync(Func<TEntity, bool> predicate) {
        var response = await _httpClient.GetAsync($"/{_entityType}");
        response.EnsureSuccessStatusCode();
        var content  = await response.Content.ReadAsStringAsync();
        var entities = JsonConvert.DeserializeObject<List<TEntity>>(content) ?? [];
        foreach (var entity in entities.Where(predicate)) yield return await Task.Run(() => entity);
    }

    public TEntity Add(TEntity entity) {
        return AddAsync(entity).GetAwaiter().GetResult();
    }

    public async Task<TEntity> AddAsync(TEntity entity) {
        var content  = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"/{_entityType}", content);

        if (response.IsSuccessStatusCode) {
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TEntity>(result) ?? throw new Exception("Failed to add Entity.");
        }

        throw new Exception($"Failed to add entity: {response.ReasonPhrase}");
    }

    public TEntity Update(TEntity entity) {
        return UpdateAsync(entity).GetAwaiter().GetResult();
    }

    public async Task<TEntity> UpdateAsync(TEntity entity) {
        var content  = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync($"/{_entityType}", content);

        if (response.IsSuccessStatusCode) {
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TEntity>(result) ?? throw new Exception("Failed to add Entity.");
        }

        throw new Exception($"Failed to add entity: {response.ReasonPhrase}");
    }

    public void Delete(string id) {
        DeleteAsync(id).GetAwaiter().GetResult();
    }

    public async Task DeleteAsync(string id) {
        var response = await _httpClient.DeleteAsync($"/{_entityType}/{id}");
        if (response.IsSuccessStatusCode) return;
        throw new Exception($"Failed to delete entity: {response.ReasonPhrase}");
    }
}