using StudentsManagement.Shared.Models;

namespace StudentsManagement.Client.Repository
{
    public interface ITeacherRepository
    {
        Task<Teacher> AddAsync(Teacher teacher);
        Task<Teacher> UpdateAsync(Teacher teacher);
        Task<Teacher> DeleteAsync(int teacherId);
        Task<Teacher> GetByIdAsync(int teacherId);
        Task<List<Teacher>> GetAllAsync();
        Task<PaginationModel<Teacher>> GetPagedTeachersAsync(int pageNumber, int pageSize);
    }
}
