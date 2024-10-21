using Microsoft.EntityFrameworkCore;
using StudentsManagement.Client.Repository;
using StudentsManagement.Data;
using StudentsManagement.Shared.Models;

namespace StudentsManagement.Services
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;
        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Book> AddAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return book;
        }

        public async Task<Book> DeleteAsync(int bookId)
        {
            var book = await _context.Books.Where(x => x.Id == bookId).FirstOrDefaultAsync();
            if (book == null) return null;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return book;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            var books = await _context.Books.Include(s => s.BookCategory).ToListAsync();

            return books;
        }

        public async Task<Book> GetByIdAsync(int bookId)
        {
            var book = await _context.Books.Include(s => s.BookCategory).FirstOrDefaultAsync(s => s.Id == bookId);

            return book;
        }

        public async Task<Book> UpdateAsync(Book book)
        {
            if (book == null) return null;


            var newbook = _context.Books.Update(book).Entity;
            await _context.SaveChangesAsync();

            return newbook;
        }

        public async Task<PaginationModel<Book>> GetPagedBooksAsync(int pageNumber, int pageSize)
        {
            var totalItems = await _context.Books.CountAsync();
            var books = await _context.Books
                .Include(s => s.BookCategory)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginationModel<Book>
            {
                Items = books,
                TotalItems = totalItems,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };
        }
    }
}
