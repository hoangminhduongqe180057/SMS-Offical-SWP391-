using StudentsManagement.Shared.Models;
using StudentsManagement.Shared.StudentRepository;
using System.Net.Http.Json;

namespace StudentsManagement.Client.Services
{
    public class StudentService : IStudentRepository
    {
        private readonly HttpClient _httpClient;
        public StudentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Student> AddStudentAsync(Student student)
        {
            var newstudent = await _httpClient.PostAsJsonAsync("api/Student/Add-Student", student);
            var respone = await newstudent.Content.ReadFromJsonAsync<Student>();
            return respone;
        }

        public async Task<Student> DeleteStudentAsync(int studentId)
        {
            var deletestudent = await _httpClient.DeleteAsync($"api/Student/Delete-Student/{studentId}");
            if (deletestudent.IsSuccessStatusCode)
            {
                var respone = await deletestudent.Content.ReadFromJsonAsync<Student>();
                return respone;
            }
            return null;
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            var allstudents = await _httpClient.GetAsync("api/Student/All-Students");
            var respone = await allstudents.Content.ReadFromJsonAsync<List<Student>>();
            return respone;
        }

        public async Task<Student> GetStudentByIdAsync(int studentId)
        {
            var singlestudent = await _httpClient.GetAsync($"api/Student/Single-Student/{studentId}");
            if (singlestudent.IsSuccessStatusCode)
            {
                var respone = await singlestudent.Content.ReadFromJsonAsync<Student>();
                return respone;
            }
            return null;
        }

        public async Task<Student> UpdateStudentAsync(Student student)
        {
            var newstudent = await _httpClient.PostAsJsonAsync("api/Student/Update-Student", student);
            var respone = await newstudent.Content.ReadFromJsonAsync<Student>();
            return respone;
        }

        public async Task<PaginationModel<Student>> GetPagedStudentsAsync(int pageNumber, int pageSize)
        {
            var response = await _httpClient.GetFromJsonAsync<PaginationModel<Student>>(
                $"api/students?pageNumber={pageNumber}&pageSize={pageSize}");
            return response;
        }


    }
}
