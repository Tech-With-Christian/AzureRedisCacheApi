namespace AzureRedisCacheApi.Helpers
{
	public interface IRedisCacheHelper
	{
		Task<T> GetCacheDataAsync<T>(string cacheKey);
		Task RemoveCacheDataAsync(string cacheKey);
		Task SetCacheDataAsync<T>(string cacheKey, T cacheValue, double absExpRelToNow, double slidingExpiration);
	}
}
