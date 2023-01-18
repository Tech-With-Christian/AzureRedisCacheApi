namespace AzureRedisCacheApi.Helpers
{
	public interface IRedisCacheHelper
	{
		/// <summary>
		/// Get cached data from Azure Redis Cache.
		/// </summary>
		/// <typeparam name="T">Generic Type</typeparam>
		/// <param name="cacheKey">Cache Key</param>
		/// <returns></returns>
		Task<T> GetCacheDataAsync<T>(string cacheKey);

		/// <summary>
		/// Remove all data related to a specific cache key in the Azure Redis Cache.
		/// </summary>
		/// <param name="cacheKey">Cache Key</param>
		Task RemoveCacheDataAsync(string cacheKey);


		/// <summary>
		/// Add data to the Azure Redis Cache by specifying a cache key to lookup in the future when requesting data from the cache.
		/// </summary>
		/// <typeparam name="T">Generic Type</typeparam>
		/// <param name="cacheKey">Cache Key</param>
		/// <param name="cacheValue">Data to Cache</param>
		/// <param name="absExpRelToNow">Absolute Expiration Relative To Now in Minutes</param>
		/// <param name="slidingExpiration">Sliding Expiration in Minutes</param>
		/// <returns></returns>
		Task SetCacheDataAsync<T>(string cacheKey, T cacheValue, double absExpRelToNow, double slidingExpiration);
	}
}
