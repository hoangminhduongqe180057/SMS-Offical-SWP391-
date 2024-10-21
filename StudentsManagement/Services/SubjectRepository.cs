using Microsoft.EntityFrameworkCore;
using StudentsManagement.Client.Repository;
using StudentsManagement.Data;
using StudentsManagement.Shared.Models;

namespace StudentsManagement.Services
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly ApplicationDbContext _context;
        public SubjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Subject> AddAsync(Subject subject)
        {
            subject.CreatedOn = DateTime.Now;
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();

            return subject;
        }

        public async Task<Subject> DeleteAsync(int subjectId)
        {
            var subject = await _context.Subjects.Where(x => x.Id == subjectId).FirstOrDefaultAsync();
            if (subject == null) return null;

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();

            return subject;
        }

        public async Task<List<Subject>> GetAllAsync()
        {
            var subjects = await _context.Subjects.ToListAsync();

            return subjects;
        }

        public async Task<Subject> GetByIdAsync(int subjectId)
        {
            var subject = await _context.Subjects.FirstOrDefaultAsync(s => s.Id == subjectId);

            return subject;
        }

        public async Task<Subject> UpdateAsync(Subject subject)
        {
            if (subject == null) return null;


            var newsubject = _context.Subjects.Update(subject).Entity;
            await _context.SaveChangesAsync();

            return newsubject;
        }

        public async Task<PaginationModel<Subject>> GetPagedSubjectsAsync(int pageNumber, int pageSize)
        {
            var totalItems = await _context.Subjects.CountAsync();
            var subjects = await _context.Subjects
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginationModel<Subject>
            {
                Items = subjects,
                TotalItems = totalItems,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };
        }
    }
}
