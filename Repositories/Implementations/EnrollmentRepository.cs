using System;
using System.Collections.Generic;
using System.Text;
using UniversityAcademicManagementSystem_Console.Data;
using UniversityAcademicManagementSystem_Console.Models;
using UniversityAcademicManagementSystem_Console.Repositories.Interfaces;

namespace UniversityAcademicManagementSystem_Console.Repositories.Implementations
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly UniversityDbContext _context;

        public EnrollmentRepository(UniversityDbContext context)
        {
            _context = context;
        }

        public void EnrollCourse(int studentId, int courseId)
        {
            if (_context.Enrollments.Any(e => e.StudentId == studentId && e.CourseId == courseId && e.EnrollmentStatus == EnrollmentStatus.ENROLLED))
                throw new InvalidOperationException("Student already enrolled in this course.");

            _context.Enrollments.Add(new Enrollment
            {
                StudentId = studentId,
                CourseId = courseId,
                EnrollmentStatus = EnrollmentStatus.ENROLLED
            });
            _context.SaveChanges();
        }

        public void DropCourse(int studentId, int courseId)
        {
            var enrollment = _context.Enrollments.FirstOrDefault(e => e.StudentId == studentId && e.CourseId == courseId && e.EnrollmentStatus == EnrollmentStatus.ENROLLED);
            if (enrollment == null)
                throw new KeyNotFoundException("Enrollment not found or already dropped.");

            enrollment.EnrollmentStatus = EnrollmentStatus.DROPPED;
            _context.SaveChanges();
        }

        public IEnumerable<Enrollment> GetEnrolledCourses(int studentId)
        {
            return _context.Enrollments.Where(e => e.StudentId == studentId && e.EnrollmentStatus == EnrollmentStatus.ENROLLED).ToList();
        }
    }
}
