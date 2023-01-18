using AzureRedisCacheApi.Entities;
using AzureRedisCacheApi.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace AzureRedisCacheApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BookController : ControllerBase
	{
		private readonly IBookRepository _bookRepo;
		private readonly IDistributedCache _redis;

		public BookController(
			IBookRepository bookRepo,
			IDistributedCache redis)
		{
			_bookRepo = bookRepo;
			_redis = redis;
		}

		[HttpGet("get")]
		public async Task<IActionResult> GetAllAsync()
		{
			// Retrieve any books stored in the Redis cache
			byte[] booksInRedis = await _redis.GetAsync("books");

			// Check that we actually got some books in return from the Redis cache
			if ((booksInRedis?.Count() ?? 0) > 0) // Amount of books is larger than 0?
			{
				// Get the books in redis as a string
				string bookString = Encoding.UTF8.GetString(booksInRedis);

				// Deserialize the string into a list of books
				List<Book> booksFromRedis = JsonSerializer.Deserialize<List<Book>>(bookString);

				// Return the books
				return Ok(new
				{
					DataFromRedis = true,
					Data = booksFromRedis
				});
			}

			// We did not have any books in our Redis Cache. Let's add some books

			// First we will get all of our books
			IReadOnlyList<Book> booksFromDb = await _bookRepo.GetBooksAsync();

			// Then serialize the books into a string
			string serializedBooks = JsonSerializer.Serialize(booksFromDb);

			// Convert the serialized books into a byte array
			byte[] booksToCache = Encoding.UTF8.GetBytes(serializedBooks);

			// Configure Expiration for Caching
			DistributedCacheEntryOptions expiration = new DistributedCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(8),
				SlidingExpiration = TimeSpan.FromMinutes(5),
			};

			// Store the books into the Redis Cache
			await _redis.SetAsync("books", booksToCache, expiration);

			// Return the books
			return Ok(new
			{
				DataFromRedis = false,
				Data = booksFromDb
			});
		}

		[HttpGet("get/{id}")]
		public async Task<IActionResult> GetSingleAsync(int id)
		{
			// Retrieve any books stored in the Redis cache
			byte[] booksInRedis = await _redis.GetAsync("books");

			// Check that we actually got some books in return from the Redis cache
			if ((booksInRedis?.Count() ?? 0) > 0)
			{
				// Decode and serialize into list of books 
				string bookString = Encoding.UTF8.GetString(booksInRedis);
				List<Book> booksFromRedis = JsonSerializer.Deserialize<List<Book>>(bookString);

				// Use LINQ to get the book we are looking for
				Book bookFromRedis = booksFromRedis.Where(x => x.Id == id).FirstOrDefault();

				if (bookFromRedis == null)
				{
					return NotFound();
				}

				// Return the book
				return Ok(new
				{
					DataFromRedis = true,
					Data = bookFromRedis
				});
			}

			// We did not get a book from the Redis Cache, let's retrieve it from our database
			Book book = await _bookRepo.GetBookAsync(id);
			if (book != null)
			{
				// Return the book we got from our database
				return Ok(new
				{
					DataFromRedis = false,
					Data = book
				});
			}

			// No book with the given ID was able to be looked up
			return NotFound();

		}

		[HttpPost("add")]
		public async Task<IActionResult> AddBookAsync(Book book)
		{
			// Add the new book to our database
			await _bookRepo.AddBookAsync(book);

			// Clear the cache
			await _redis.RemoveAsync("books");

			// Return the created book
			return Ok(new
			{
				DataFromRedis = false,
				Data = book
			});
		}

		[HttpPut("update")]
		public async Task<IActionResult> UpdateBookAsync(Book book)
		{
			// Update the book in our database
			await _bookRepo.UpdateBookAsync(book);

			// Clear the cache
			await _redis.RemoveAsync("books");

			// Return the updated book
			return Ok(new
			{
				DataFromRedis = false,
				Data = book
			});
		}

		[HttpDelete("delete/{id}")]
		public async Task<bool> DeleteBookAsync(int id)
		{
			return await _bookRepo.DeleteBookAsync(id);
		}
	}
}
