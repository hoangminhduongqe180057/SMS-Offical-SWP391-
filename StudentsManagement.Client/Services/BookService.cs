using StudentsManagement.Client.Repository;
using StudentsManagement.Shared.Models;
using System.Net.Http.Json;
using static System.Reflection.Metadata.BlobBuilder;

namespace StudentsManagement.Client.Services
{
    public class BookService : IBookRepository
    {
        private readonly HttpClient _httpClient;
        public BookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Book> AddAsync(Book book)
        {
            var data = await _httpClient.PostAsJsonAsync("api/Books/Add-Book", book);
            var respone = await data.Content.ReadFromJsonAsync<Book>();
            return respone;
        }

        public async Task<Book> DeleteAsync(int bookId)
        {
            var data = await _httpClient.PostAsJsonAsync("api/Books/Delete-Book", bookId);
            var respone = await data.Content.ReadFromJsonAsync<Book>();
            return respone;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            var data = await _httpClient.GetAsync("api/Books/All-Books");
            var respone = await data.Content.ReadFromJsonAsync<List<Book>>();
            return respone;
        }

        public async Task<Book> GetByIdAsync(int bookId)
        {
            var data = await _httpClient.GetAsync($"api/Books/Single-Book/{bookId}");
            var respone = await data.Content.ReadFromJsonAsync<Book>();
            return respone;
        }

        public async Task<Book> UpdateAsync(Book book)
        {
            var data = await _httpClient.PostAsJsonAsync("api/Books/Update-Book", book);
            var respone = await data.Content.ReadFromJsonAsync<Book>();
            return respone;
        }

        public async Task<PaginationModel<Book>> GetPagedBooksAsync(int pageNumber, int pageSize)
        {
            var response = await _httpClient.GetFromJsonAsync<PaginationModel<Book>>(
                $"api/book?pageNumber={pageNumber}&pageSize={pageSize}");
            return response;
        }
    }
}
