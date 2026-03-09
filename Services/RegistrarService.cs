using System;
using System.Linq;
using UniversityAcademicManagementSystem_Console.Models;
using UniversityAcademicManagementSystem_Console.Repositories;

namespace UniversityAcademicManagementSystem_Console.Services
{
	public class RegistrarService
	{
		private readonly CourseRepository _courseRepo;
		private readonly EnrollmentRepository _enrollmentRepo;

		public RegistrarService(CourseRepository courseRepo,
								EnrollmentRepository enrollmentRepo)
		{
			_courseRepo = courseRepo;
			_enrollmentRepo = enrollmentRepo;
		}

		// 1. Add Course
		public void AddCourse(Course course)
		{
			_courseRepo.Add(course);
			Console.WriteLine("Course added successfully!");
		}

		// 2. Update Course
		public void UpdateCourse(Course course)
		{
			var existing = _courseRepo.GetById(course.CourseId);
			if (existing == null)
			{
				Console.WriteLine("Course not found.");
				return;
			}

			existing.CourseName = course.CourseName;
			existing.Credits = course.Credits;
			existing.Department = course.Department;
			existing.SemesterOffered = course.SemesterOffered;

			_courseRepo.Update(existing);
			Console.WriteLine("Course updated successfully!");
		}

		// 3. List Courses
		public void ListCourses()
		{
			var courses = _courseRepo.GetAll();
			if (!courses.Any())
			{
				Console.WriteLine("No courses available.");
				return;
			}

			Console.WriteLine("--- Course Catalog ---");
			foreach (var c in courses)
			{
				Console.WriteLine($"[{c.CourseId}] {c.CourseName} | Credits: {c.Credits} | Dept: {c.Department} | Semester: {c.SemesterOffered}");
			}
			Console.WriteLine("----------------------");
		}

		// 4. Assign Course to Semester
		public void AssignCourseToSemester(int courseId, string semester)
		{
			var course = _courseRepo.GetById(courseId);
			if (course == null)
			{
				Console.WriteLine("Course not found.");
				return;
			}

			course.SemesterOffered = semester;
			_courseRepo.Update(course);
			Console.WriteLine($"Course {course.CourseName} assigned to {semester} semester.");
		}

		// 5. View Enrollment Records
		public void ViewEnrollmentRecords()
		{
			var enrollments = _enrollmentRepo.GetAll();
			if (!enrollments.Any())
			{
				Console.WriteLine("No enrollment records found.");
				return;
			}

			Console.WriteLine("--- Enrollment Records ---");
			foreach (var e in enrollments)
			{
				Console.WriteLine($"EnrollmentId: {e.EnrollmentId} | StudentId: {e.StudentId} | CourseId: {e.CourseId} | Status: {e.EnrollmentStatus}");
			}
			Console.WriteLine("--------------------------");
		}
	}
}
