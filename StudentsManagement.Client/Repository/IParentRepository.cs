using StudentsManagement.Shared.Models;

namespace StudentsManagement.Client.Repository
{
    public interface IParentRepository
    {
        Task<Parent> AddAsync(Parent parent);
        Task<Parent> UpdateAsync(Parent parent);
        Task<Parent> DeleteAsync(int parentId);
        Task<Parent> GetByIdAsync(int parentId);
        Task<List<Parent>> GetAllAsync();
        Task<PaginationModel<Parent>> GetPagedParentsAsync(int pageNumber, int pageSize);
    }
}
