using AzureRedisCacheApi.Entities;
using AzureRedisCacheApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureRedisCacheApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BookController : ControllerBase
	{
		private readonly IBookService _book;

		public BookController(IBookService book)
		{
			_book = book;
		}

		[HttpGet("get")]
		public async Task<IActionResult> GetAllAsync()
		{
			// Get the books from either the cache or database
			var (books, fromCache) = await _book.GetBooksAsync();

			// Check if we got any books from the service
			if (books == null)
			{
				// We did not get any books
				return NotFound("No books has been added to the service.");
			}

			// We got some books, let's return them
			return Ok(new
			{
				DataFromCache = fromCache,
				Data = books
			});
		}

		[HttpGet("get/{id}")]
		public async Task<IActionResult> GetSingleAsync(int id)
		{
			// Get the books from either the cache or database
			var (book, fromCache) = await _book.GetBookAsync(id);

			// Check if we got any books from the service
			if (book == null)
			{
				// We did no get a book matching the id specified
				return NotFound($"No book was found with the id: {id}.");
			}

			// We got a book matching the id, let's return it
			return Ok(new
			{
				DataFromCache = fromCache,
				Data = book
			});

		}

		[HttpPost("add")]
		public async Task<IActionResult> AddBookAsync(Book book)
		{
			// Add the new book
			await _book.AddBookAsync(book);
			return Ok(book);
		}

		[HttpPut("update")]
		public async Task<IActionResult> UpdateBookAsync(Book book)
		{
			// Update the book in our database
			await _book.UpdateBookAsync(book);
			return Ok(book);

		}

		[HttpDelete("delete/{id}")]
		public async Task<bool> DeleteBookAsync(int id)
		{
			// Delete the book
			return await _book.DeleteBookAsync(id);
		}
	}
}
