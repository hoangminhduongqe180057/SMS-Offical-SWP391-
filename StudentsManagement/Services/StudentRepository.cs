using Microsoft.EntityFrameworkCore;
using StudentsManagement.Client.Repository;
using StudentsManagement.Data;
using StudentsManagement.Shared.Models;
using StudentsManagement.Shared.StudentRepository;

namespace StudentsManagement.Services
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<Student> AddStudentAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return student;
        }


        public async Task<Student> DeleteStudentAsync(int studentId)
        {
            var student = await _context.Students.Where(x => x.Id == studentId).FirstOrDefaultAsync();
            if (student == null) return null;

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return student;
        }


        public async Task<List<Student>> GetAllStudentsAsync()
        {
            var students = await _context.Students.Include(s => s.Gender).Include(s => s.Country).ToListAsync();

            return students;
        }

        public async Task<Student> GetStudentByIdAsync(int studentId)
        {
            var student = await _context.Students
        .Include(s => s.Gender) // Chắc chắn bao gồm thông tin giới tính
        .Include(s => s.Country) // Chắc chắn bao gồm thông tin quốc gia
        .FirstOrDefaultAsync(s => s.Id == studentId);

            return student;
        }

        public async Task<Student> UpdateStudentAsync(Student student)
        {
            if (student == null) return null;


            var newstudent = _context.Students.Update(student).Entity;
            await _context.SaveChangesAsync();

            return newstudent;
        }


        public async Task<PaginationModel<Student>> GetPagedStudentsAsync(int pageNumber, int pageSize)
        {
            var totalItems = await _context.Students.CountAsync(); 
            var students = await _context.Students
                .Include(s => s.Gender)    
                .Include(s => s.Country)   
                .Skip((pageNumber - 1) * pageSize) 
                .Take(pageSize)            
                .ToListAsync();

            return new PaginationModel<Student>
            {
                Items = students,
                TotalItems = totalItems,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };
        }
    }
}
