using System;
using System.Linq;
using UniversityAcademicManagementSystem_Console.Models;
using UniversityAcademicManagementSystem_Console.Repositories;

namespace UniversityAcademicManagementSystem_Console.Services
{
	public class AdminService
	{
		private readonly StudentRepository _studentRepo;
		private readonly CourseRepository _courseRepo;

		public AdminService(StudentRepository studentRepo, CourseRepository courseRepo)
		{
			_studentRepo = studentRepo;
			_courseRepo = courseRepo;
		}

		// 1. View All Students
		public void ViewAllStudents()
		{
			var students = _studentRepo.GetAll();
			if (!students.Any())
			{
				Console.WriteLine("No students found.");
				return;
			}

			Console.WriteLine("--- All Students ---");
			foreach (var s in students)
			{
				Console.WriteLine($"[{s.StudentId}] {s.Name} | {s.Email} | Dept: {s.Department} | Year: {s.EnrollmentYear}");
			}
			Console.WriteLine("--------------------");
		}

		// 2. Remove Student
		public void RemoveStudent(int studentId)
		{
			var student = _studentRepo.GetById(studentId);
			if (student == null)
			{
				Console.WriteLine("Student not found.");
				return;
			}

			_studentRepo.Delete(studentId);
			Console.WriteLine($"Student {student.Name} removed successfully.");
		}

		// 3. View All Courses
		public void ViewAllCourses()
		{
			var courses = _courseRepo.GetAll();
			if (!courses.Any())
			{
				Console.WriteLine("No courses found.");
				return;
			}

			Console.WriteLine("--- All Courses ---");
			foreach (var c in courses)
			{
				Console.WriteLine($"[{c.CourseId}] {c.CourseName} | Credits: {c.Credits} | Dept: {c.Department} | Semester: {c.SemesterOffered}");
			}
			Console.WriteLine("-------------------");
		}

		// 4. Remove Course
		public void RemoveCourse(int courseId)
		{
			var course = _courseRepo.GetById(courseId);
			if (course == null)
			{
				Console.WriteLine("Course not found.");
				return;
			}

			_courseRepo.Delete(courseId);
			Console.WriteLine($"Course {course.CourseName} removed successfully.");
		}
	}
}
