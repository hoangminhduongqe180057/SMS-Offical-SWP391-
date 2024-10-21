using Microsoft.EntityFrameworkCore;
using StudentsManagement.Client.Repository;
using StudentsManagement.Data;
using StudentsManagement.Shared.Models;

namespace StudentsManagement.Services
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;
        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Department> AddAsync(Department department)
        {
            department.CreatedOn = DateTime.Now;
            if (department == null) return null;
            var data = _context.Departments.Add(department).Entity;
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<Department> DeleteAsync(int departmentId)
        {

            var data = await _context.Departments.Where(x => x.Id == departmentId).FirstOrDefaultAsync();
            if (data == null) return null;
            _context.Departments.Remove(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<List<Department>> GetAllAsync()
        {
            var data = await _context.Departments.ToListAsync();
            return data;
        }

        public async Task<Department> GetByIdAsync(int departmentId)
        {
            var data = await _context.Departments.FirstOrDefaultAsync(x => x.Id == departmentId);
            return data;
        }

        public async Task<Department> UpdateAsync(Department department)
        {
            if (department == null) return null;
            var data = _context.Departments.Update(department).Entity;
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<PaginationModel<Department>> GetPagedDepartmentsAsync(int pageNumber, int pageSize)
        {
            var totalItems = await _context.Departments.CountAsync();
            var departments = await _context.Departments
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginationModel<Department>
            {
                Items = departments,
                TotalItems = totalItems,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };
        }
    }
}
