using AzureRedisCacheApi.Entities;

namespace AzureRedisCacheApi.Services.Repositories
{
	public interface IBookRepository
	{
		/// <summary>
		/// Get all books from the database in a read only list.
		/// </summary>
		/// <returns>Read only list of all books in database</returns>
		Task<IReadOnlyList<Book>> GetBooksAsync();

		/// <summary>
		/// Get a specific by by the id of the book.
		/// </summary>
		/// <param name="bookId">Book ID</param>
		/// <returns>Book Entity</returns>
		Task<Book> GetBookAsync(int bookId);

		/// <summary>
		/// Add a new book to the database.
		/// </summary>
		/// <param name="book">Book Entity</param>
		/// <returns>Book Entity</returns>
		Task<Book> AddBookAsync(Book book);

		/// <summary>
		/// Update a book in the database.
		/// </summary>
		/// <param name="book">Book Entity</param>
		/// <returns>Book Entity</returns>
		Task<Book> UpdateBookAsync(Book book);

		/// <summary>
		/// Delete a book in the database.
		/// </summary>
		/// <param name="bookId">Book Entity</param>
		/// <returns>True if deleted</returns>
		Task<bool> DeleteBookAsync(int bookId);
	}
}
