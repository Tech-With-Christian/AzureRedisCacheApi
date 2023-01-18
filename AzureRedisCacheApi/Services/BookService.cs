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

		/// <summary>
		/// Add a new book to the database and clear the Redis cache at Azure.
		/// </summary>
		/// <param name="book">Book Entity</param>
		/// <returns>Book from database</returns>
		public async Task<Book> AddBookAsync(Book book)
		{
			await _bookRepo.AddBookAsync(book);
			await _redis.RemoveCacheDataAsync(_cacheKey);
			return book;
		}

		/// <summary>
		/// Delete a book in the database and clear the cache for any books
		/// </summary>
		/// <param name="bookId">Book ID</param>
		/// <returns>True if book got deleted</returns>
		public async Task<bool> DeleteBookAsync(int bookId)
		{
			bool result = await _bookRepo.DeleteBookAsync(bookId);
			await _redis.RemoveCacheDataAsync(_cacheKey);

			return result;
		}

		/// <summary>
		/// Get a book from the cache or database. If the cache doesn't contain a book
		/// matching the id provided, a request will be made to the database.
		/// </summary>
		/// <param name="bookId">Book ID</param>
		/// <returns>Book Entity and true if data was loaded from Azure Redis cache</returns>
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

		/// <summary>
		/// Get all boks stored in either the cache or local application database.
		/// </summary>
		/// <returns>Read Only List of Book Entity and true if data was loaded from cache</returns>
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

		/// <summary>
		/// Update a book in the database and clear the book cache.
		/// </summary>
		/// <param name="book">Book Entity</param>
		/// <returns>Book Entity from database</returns>
		public async Task<Book> UpdateBookAsync(Book book)
		{
			await _bookRepo.UpdateBookAsync(book);
			await _redis.RemoveCacheDataAsync(_cacheKey);
			return book;
		}
	}
}
