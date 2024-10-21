using StudentsManagement.Client.Repository;
using StudentsManagement.Shared.Models;
using System.Net.Http.Json;

namespace StudentsManagement.Client.Services
{
    public class TeacherService : ITeacherRepository
    {
        private readonly HttpClient _httpClient;
        public TeacherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Teacher> AddAsync(Teacher teacher)
        {
            var data = await _httpClient.PostAsJsonAsync("api/Teacher/Add-Teacher", teacher);
            var respone = await data.Content.ReadFromJsonAsync<Teacher>();
            return respone;
        }

        public async Task<Teacher> DeleteAsync(int teacherId)
        {
            var data = await _httpClient.DeleteAsync($"api/Teacher/Delete-Teacher/{teacherId}");
            var respone = await data.Content.ReadFromJsonAsync<Teacher>();
            return respone;
        }

        public async Task<List<Teacher>> GetAllAsync()
        {
            var data = await _httpClient.GetAsync("api/Teacher/All-Teachers");
            var respone = await data.Content.ReadFromJsonAsync<List<Teacher>>();
            return respone;
        }

        public async Task<Teacher> GetByIdAsync(int teacherId)
        {
            var data = await _httpClient.GetAsync($"api/Teacher/Single-Teacher/{teacherId}");
            var respone = await data.Content.ReadFromJsonAsync<Teacher>();
            return respone;
        }

        public async Task<Teacher> UpdateAsync(Teacher teacher)
        {
            var data = await _httpClient.PostAsJsonAsync("api/Teacher/Update-Teacher", teacher);
            var respone = await data.Content.ReadFromJsonAsync<Teacher>();
            return respone;
        }

        public async Task<PaginationModel<Teacher>> GetPagedTeachersAsync(int pageNumber, int pageSize)
        {
            var response = await _httpClient.GetFromJsonAsync<PaginationModel<Teacher>>(
                $"api/teacher?pageNumber={pageNumber}&pageSize={pageSize}");
            return response;
        }
    }
}
