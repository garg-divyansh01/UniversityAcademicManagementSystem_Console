using System;
using System.Collections.Generic;
using System.Text;
using UniversityAcademicManagementSystem_Console.Data;
using UniversityAcademicManagementSystem_Console.Models;
using UniversityAcademicManagementSystem_Console.Repositories.Interfaces;

namespace UniversityAcademicManagementSystem_Console.Repositories.Implementations
{
    public class GradeRepository : IGradeRepository
    {
        private readonly UniversityDbContext _context;

        public GradeRepository(UniversityDbContext context)
        {
            _context = context;
        }

        public void SubmitGrade(Grade grade)
        {
            if (string.IsNullOrWhiteSpace(grade.GradeValue))
                throw new ArgumentException("Grade cannot be empty.");

            _context.Grades.Add(grade);
            _context.SaveChanges();
        }

        public void UpdateGrade(Grade grade)
        {
            var existing = _context.Grades.Find(grade.GradeId);
            if (existing == null)
                throw new KeyNotFoundException($"Grade with ID {grade.GradeId} not found.");

            existing.GradeValue = grade.GradeValue;
            existing.Remarks = grade.Remarks;
            _context.SaveChanges();
        }

        public IEnumerable<Grade> GetGradesByCourse(int courseId)
        {
            return _context.Grades.Where(g => g.CourseId == courseId).ToList();
        }
    }
}
