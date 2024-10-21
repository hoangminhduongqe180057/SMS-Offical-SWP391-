using StudentsManagement.Client.Repository;
using StudentsManagement.Shared.Models;
using System.Net.Http.Json;

namespace StudentsManagement.Client.Services
{
    public class ParentService : IParentRepository
    {
        private readonly HttpClient _httpClient;
        public ParentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Parent> AddAsync(Parent parent)
        {
            var newparent = await _httpClient.PostAsJsonAsync("api/Parent/Add-Parent", parent);
            var respone = await newparent.Content.ReadFromJsonAsync<Parent>();
            return respone;
        }

        public async Task<Parent> DeleteAsync(int parentId)
        {
            var deleteparent = await _httpClient.DeleteAsync($"api/Parent/Delete-Parent/{parentId}");
            var respone = await deleteparent.Content.ReadFromJsonAsync<Parent>();
            return respone;
        }

        public async Task<List<Parent>> GetAllAsync()
        {
            var allparents = await _httpClient.GetAsync("api/Parent/All-Parents");
            var respone = await allparents.Content.ReadFromJsonAsync<List<Parent>>();
            return respone;
        }

        public async Task<Parent> GetByIdAsync(int parentId)
        {
            var singleparent = await _httpClient.GetAsync($"api/Parent/Single-Parent/{parentId}");
            var respone = await singleparent.Content.ReadFromJsonAsync<Parent>();
            return respone;
        }

        public async Task<Parent> UpdateAsync(Parent parent)
        {
            var newparent = await _httpClient.PostAsJsonAsync("api/Parent/Update-Parent", parent);
            var respone = await newparent.Content.ReadFromJsonAsync<Parent>();
            return respone;
        }

        public async Task<PaginationModel<Parent>> GetPagedParentsAsync(int pageNumber, int pageSize)
        {
            var response = await _httpClient.GetFromJsonAsync<PaginationModel<Parent>>(
                $"api/parent?pageNumber={pageNumber}&pageSize={pageSize}");
            return response;
        }
    }
}
