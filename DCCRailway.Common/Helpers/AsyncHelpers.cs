namespace DCCRailway.Common.Helpers;

public static class AsyncHelpers {
    public static async Task<List<T>> GetListFromAsyncEnumerable<T>(this IAsyncEnumerable<T> asyncEnumerable) {
        var list = new List<T>();
        await foreach (var item in asyncEnumerable) {
            list.Add(item);
        }
        return list;
    }
}