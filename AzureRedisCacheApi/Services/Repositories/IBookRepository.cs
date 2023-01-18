using AzureRedisCacheApi.Entities;
using AzureRedisCacheApi.Persistence;

namespace AzureRedisCacheApi.Services.Repositories
{
	public interface IBookRepository
	{
		Task<IReadOnlyList<Book>> GetBooksAsync();
		Task<Book> GetBookAsync(int bookId);
		Task<Book> AddBookAsync(Book book);
		Task<Book> UpdateBookAsync(Book book);
		Task<bool> DeleteBookAsync(int bookId);
	}
}
