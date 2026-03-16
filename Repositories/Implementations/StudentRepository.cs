using System.Collections.Generic;
using System.Linq;
using UniversityAcademicManagementSystem_Console.Data;
using UniversityAcademicManagementSystem_Console.Models;
using UniversityAcademicManagementSystem_Console.Repositories.Interfaces;

namespace UniversityAcademicManagementSystem_Console.Repositories.Implementations
{
    public class StudentRepository : IStudentRepository
    {
        private readonly UniversityDbContext _context;

        public StudentRepository(UniversityDbContext context)
        {
            _context = context;
        }

        public void RegisterStudent(Student student)
        {
            if (string.IsNullOrWhiteSpace(student.Name) || string.IsNullOrWhiteSpace(student.Email) || string.IsNullOrWhiteSpace(student.Department) || string.IsNullOrWhiteSpace(student.ContactNumber) || student.EnrollmentYear == 0)
                throw new ArgumentException("Invalid Input");

            var existing = _context.Students.FirstOrDefault(s => s.Email == student.Email);
            if (existing != null)
                throw new InvalidOperationException("Email already registered.");

            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public void UpdateProfile(Student student)
        {
            var existing = _context.Students.Find(student.StudentId);
            if (existing == null)
                throw new KeyNotFoundException($"Student with ID {student.StudentId} not found.");

            if (string.IsNullOrWhiteSpace(student.Name) || string.IsNullOrWhiteSpace(student.Email) || string.IsNullOrWhiteSpace(student.Department) || string.IsNullOrWhiteSpace(student.ContactNumber) || student.EnrollmentYear == 0)
                throw new ArgumentException("Invalid Input");

            existing.Name = student.Name;
            existing.Email = student.Email;
            existing.Department = student.Department;
            existing.ContactNumber = student.ContactNumber;
            existing.EnrollmentYear = student.EnrollmentYear;

            _context.SaveChanges();
        }

        public Student GetStudentDetails(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
                throw new KeyNotFoundException($"Student with ID {id} not found.");
            return student;
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _context.Students.ToList();
        }
    }
}
