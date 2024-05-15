namespace DCCRailway.Common.Helpers;

public static class AsyncHelpers {
    public static async Task<IList<T>> GetListFromAsyncEnumerable<T>(this IAsyncEnumerable<T> asyncEnumerable) {
        var list = new List<T>();
        await foreach (var item in asyncEnumerable) {
            list.Add(item);
        }
        return list;
    }

    public static async IAsyncEnumerable<T> ConvertToAsyncEnumerable<T>(this IEnumerable<T> enumerable) {
        foreach (var item in enumerable) {
            await Task.Yield(); // This is used to create an artificial delay. In real world you might have other awaitable operations.
            yield return item;
        }
    }
}