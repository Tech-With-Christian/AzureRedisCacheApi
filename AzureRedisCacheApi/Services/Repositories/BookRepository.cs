using AzureRedisCacheApi.Entities;
using AzureRedisCacheApi.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AzureRedisCacheApi.Services.Repositories
{
	public class BookRepository : IBookRepository
	{
		private readonly AppDbContext _appDbContext;

		public BookRepository(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		public async Task<Book> AddBookAsync(Book book)
		{
			await _appDbContext.Books.AddAsync(book);
			await _appDbContext.SaveChangesAsync();
			return book;
		}

		public async Task<bool> DeleteBookAsync(int bookId)
		{
			var book = await GetBookAsync(bookId);
			var deleteResult = _appDbContext.Books.Remove(book);
			await _appDbContext.SaveChangesAsync();
			return deleteResult != null ? true : false;
		}

		public async Task<Book> GetBookAsync(int bookId)
		{
			return await _appDbContext.Books.FirstOrDefaultAsync(x => x.Id == bookId);
		}

		public async Task<IReadOnlyList<Book>> GetBooksAsync()
		{
			return await _appDbContext.Books.ToListAsync();
		}

		public async Task<Book> UpdateBookAsync(Book book)
		{
			var updateResult = _appDbContext.Books.Update(book);
			await _appDbContext.SaveChangesAsync();
			return updateResult.Entity;
		}
	}
}
