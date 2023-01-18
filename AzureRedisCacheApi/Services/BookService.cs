using AzureRedisCacheApi.Entities;
using AzureRedisCacheApi.Helpers;
using AzureRedisCacheApi.Services.Repositories;

namespace AzureRedisCacheApi.Services
{
	public class BookService : IBookService
	{
		private readonly IRedisCacheHelper _redis;
		private readonly IBookRepository _bookRepo;
		private readonly string _cacheKey = "books";

		public BookService(IRedisCacheHelper redis, IBookRepository bookRepo)
		{
			_redis = redis;
			_bookRepo = bookRepo;
		}

		public async Task<Book> AddBookAsync(Book book)
		{
			await _bookRepo.AddBookAsync(book);
			await _redis.RemoveCacheDataAsync(_cacheKey);
			return book;
		}

		public async Task<bool> DeleteBookAsync(int bookId)
		{
			bool result = await _bookRepo.DeleteBookAsync(bookId);
			await _redis.RemoveCacheDataAsync(_cacheKey);

			return result;
		}

		public async Task<(Book, bool)> GetBookAsync(int bookId)
		{
			var cacheData = await _redis.GetCacheDataAsync<IReadOnlyList<Book>>(_cacheKey);
			// Check if the cache data contains any number of books
			if (cacheData != null)
			{
				Book bookFromCache = cacheData.Where(x => x.Id == bookId).FirstOrDefault();

				if (bookFromCache == null)
				{
					// There was no book matching our requested book id
					return (null, false);
				}

				// We got a book matching the id, let's return the book
				return (bookFromCache, true);
			}

			// No books in the cache.
			Book book = await _bookRepo.GetBookAsync(bookId);

			if (book == null)
			{
				return (null, false);
			}

			// We got a book from the database matching the requested book id
			return (book, false);
		}

		public async Task<(IReadOnlyList<Book>, bool)> GetBooksAsync()
		{
			var cacheData = await _redis.GetCacheDataAsync<IReadOnlyList<Book>>(_cacheKey);

			// Check if the cache data contains any number of books
			if (cacheData != null)
			{
				// We got data, let's return the book(s)
				return (cacheData, true);
			}

			// No cache data was available, set the cache data
			IReadOnlyList<Book> booksFromDb = await _bookRepo.GetBooksAsync();
			await _redis.SetCacheDataAsync(_cacheKey, booksFromDb, 10, 5);

			// Return the books from our database
			return (booksFromDb, false);
		}

		public async Task<Book> UpdateBookAsync(Book book)
		{
			await _bookRepo.UpdateBookAsync(book);
			await _redis.RemoveCacheDataAsync(_cacheKey);
			return book;
		}
	}
}
