using System;
using System.Linq;
using UniversityAcademicManagementSystem_Console.Models;
using UniversityAcademicManagementSystem_Console.Repositories;

namespace UniversityAcademicManagementSystem_Console.Services
{
	public class FacultyService
	{
		private readonly CourseRepository _courseRepo;
		private readonly EnrollmentRepository _enrollmentRepo;
		private readonly GradeRepository _gradeRepo;

		public FacultyService(CourseRepository courseRepo,
							  EnrollmentRepository enrollmentRepo,
							  GradeRepository gradeRepo)
		{
			_courseRepo = courseRepo;
			_enrollmentRepo = enrollmentRepo;
			_gradeRepo = gradeRepo;
		}

		// 1. List My Courses
		public void ListMyCourses(string department, string semester)
		{
			var courses = _courseRepo.GetBySemester(semester)
				.Where(c => c.Department.Equals(department, StringComparison.OrdinalIgnoreCase));

			if (!courses.Any())
			{
				Console.WriteLine($"No courses found for {department} in {semester}.");
				return;
			}

			Console.WriteLine($"--- Courses for {department} ({semester}) ---");
			foreach (var c in courses)
			{
				Console.WriteLine($"[{c.CourseId}] {c.CourseName} | Credits: {c.Credits}");
			}
			Console.WriteLine("---------------------------------------------");
		}

		// 2. View Enrolled Students
		public void ViewEnrolledStudents(int courseId)
		{
			var enrollments = _enrollmentRepo.GetAll()
				.Where(e => e.CourseId == courseId && e.EnrollmentStatus == EnrollmentStatus.ENROLLED);

			if (!enrollments.Any())
			{
				Console.WriteLine($"No students enrolled in CourseId {courseId}.");
				return;
			}

			Console.WriteLine($"--- Enrolled Students for Course {courseId} ---");
			foreach (var e in enrollments)
			{
				Console.WriteLine($"StudentId: {e.StudentId} | Name: {e.Student?.Name}");
			}
			Console.WriteLine("-----------------------------------------------");
		}

		// 3. Submit Grade
		public void SubmitGrade(Grade grade)
		{
			var existing = _gradeRepo.GetAll()
				.FirstOrDefault(g => g.StudentId == grade.StudentId && g.CourseId == grade.CourseId);

			if (existing != null)
			{
				Console.WriteLine("Grade already exists. Use UpdateGrade instead.");
				return;
			}

			_gradeRepo.Add(grade);
			Console.WriteLine("Grade submitted successfully!");
		}

		// 4. Update Grade
		public void UpdateGrade(Grade grade)
		{
			var existing = _gradeRepo.GetById(grade.GradeId);
			if (existing == null)
			{
				Console.WriteLine("Grade record not found.");
				return;
			}

			existing.GradeValue = grade.GradeValue;
			existing.Remarks = grade.Remarks;
			_gradeRepo.Update(existing);

			Console.WriteLine("Grade updated successfully!");
		}

		// 5. View Grades by Course
		public void ViewGradesByCourse(int courseId)
		{
			var grades = _gradeRepo.GetAll().Where(g => g.CourseId == courseId);

			if (!grades.Any())
			{
				Console.WriteLine($"No grades found for CourseId {courseId}.");
				return;
			}

			Console.WriteLine($"--- Grades for Course {courseId} ---");
			foreach (var g in grades)
			{
				Console.WriteLine($"StudentId: {g.StudentId} | Grade: {g.GradeValue} | Remarks: {g.Remarks}");
			}
			Console.WriteLine("------------------------------------");
		}
	}
}
