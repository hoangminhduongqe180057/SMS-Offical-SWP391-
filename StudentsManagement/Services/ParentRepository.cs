using Microsoft.EntityFrameworkCore;
using StudentsManagement.Client.Repository;
using StudentsManagement.Data;
using StudentsManagement.Shared.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;

namespace StudentsManagement.Services
{
    public class ParentRepository : IParentRepository
    {
        private readonly ApplicationDbContext _context;

        public ParentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Parent> AddAsync(Parent parent)
        {
            if (parent == null) return null;
            var newparent = _context.Parents.Add(parent).Entity;
            await _context.SaveChangesAsync();
            return newparent;
        }

        public async Task<Parent> DeleteAsync(int parentId)
        {
            var parent = await _context.Parents.Where(x => x.Id == parentId).FirstOrDefaultAsync();
            if (parent == null) return null;
            _context.Parents.Remove(parent);
            await _context.SaveChangesAsync();
            return parent;
        }

        public async Task<List<Parent>> GetAllAsync()
        {
            var data = await _context.Parents
                .Include(x => x.Student)
                .ToListAsync();

            return data;
        }

        public async Task<Parent> GetByIdAsync(int parentId)
        {
            var data = await _context.Parents.Include(s => s.Gender).Include(x => x.Student).Include(z => z.ParentType).Where(x => x.Id == parentId).FirstOrDefaultAsync();
            if (data == null) return null;

            return data;
        }

        public async Task<Parent> UpdateAsync(Parent parent)
        {
            if (parent == null) return null;

            var data = _context.Parents.Update(parent).Entity;
            await _context.SaveChangesAsync();

            return data;
        }

        public async Task<PaginationModel<Parent>> GetPagedParentsAsync(int pageNumber, int pageSize)
        {
            var totalItems = await _context.Parents.CountAsync();
            var parents = await _context.Parents
                .Include(g => g.Gender)
                .Include(s => s.Student)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginationModel<Parent>
            {
                Items = parents,
                TotalItems = totalItems,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };
        }
    }
}
