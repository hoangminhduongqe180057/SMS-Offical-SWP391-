using StudentsManagement.Client.Repository;
using StudentsManagement.Shared.Models;
using System.Net.Http.Json;

namespace StudentsManagement.Client.Services

{
    public class SubjectService : ISubjectRepository
    {
        private readonly HttpClient _httpClient;
        public SubjectService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Subject> AddAsync(Subject subject)
        {
            var data = await _httpClient.PostAsJsonAsync("api/Subjects/Add-Subject", subject);
            var respone = await data.Content.ReadFromJsonAsync<Subject>();
            return respone;
        }

        public async Task<Subject> DeleteAsync(int subjectId)
        {
            var data = await _httpClient.PostAsJsonAsync("api/Subjects/Delete-Subject", subjectId);
            var respone = await data.Content.ReadFromJsonAsync<Subject>();
            return respone;
        }

        public async Task<List<Subject>> GetAllAsync()
        {
            var data = await _httpClient.GetAsync("api/Subjects/All-Subjects");
            var respone = await data.Content.ReadFromJsonAsync<List<Subject>>();
            return respone;
        }

        public async Task<Subject> GetByIdAsync(int subjectId)
        {
            var data = await _httpClient.GetAsync($"api/Subjects/Single-Subject/{subjectId}");
            var respone = await data.Content.ReadFromJsonAsync<Subject>();
            return respone;
        }

        public async Task<Subject> UpdateAsync(Subject subject)
        {
            var data = await _httpClient.PostAsJsonAsync("api/Subjects/Update-Subject", subject);
            var respone = await data.Content.ReadFromJsonAsync<Subject>();
            return respone;
        }

        public async Task<PaginationModel<Subject>> GetPagedSubjectsAsync(int pageNumber, int pageSize)
        {
            var response = await _httpClient.GetFromJsonAsync<PaginationModel<Subject>>(
                $"api/subject?pageNumber={pageNumber}&pageSize={pageSize}");
            return response;
        }
    }
}
