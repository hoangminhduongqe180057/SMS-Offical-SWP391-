using StudentsManagement.Client.Repository;
using StudentsManagement.Shared.Models;
using System.Net.Http.Json;

namespace StudentsManagement.Client.Services
{
    public class DepartmentService : IDepartmentRepository
    {
        private readonly HttpClient _httpClient;
        public DepartmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Department> AddAsync(Department department)
        {
            var data = await _httpClient.PostAsJsonAsync("api/Departments/Add-Department", department);
            var respone = await data.Content.ReadFromJsonAsync<Department>();
            return respone;
        }

        public async Task<Department> DeleteAsync(int departmentId)
        {
            var data = await _httpClient.PostAsJsonAsync("api/Departments/Delete-Department", departmentId);
            var respone = await data.Content.ReadFromJsonAsync<Department>();
            return respone;
        }

        public async Task<List<Department>> GetAllAsync()
        {
            var data = await _httpClient.GetAsync("api/Departments/All-Departments");
            var respone = await data.Content.ReadFromJsonAsync<List<Department>>();
            return respone;
        }

        public async Task<Department> GetByIdAsync(int departmentId)
        {
            var data = await _httpClient.GetAsync($"api/Departments/Single-Department/{departmentId}");
            var respone = await data.Content.ReadFromJsonAsync<Department>();
            return respone;
        }

        public async Task<Department> UpdateAsync(Department department)
        {
            var data = await _httpClient.PostAsJsonAsync("api/Departments/Update-Department", department);
            var respone = await data.Content.ReadFromJsonAsync<Department>();
            return respone;
        }

        public async Task<PaginationModel<Department>> GetPagedDepartmentsAsync(int pageNumber, int pageSize)
        {
            var response = await _httpClient.GetFromJsonAsync<PaginationModel<Department>>(
                $"api/department?pageNumber={pageNumber}&pageSize={pageSize}");
            return response;
        }
    }
}
