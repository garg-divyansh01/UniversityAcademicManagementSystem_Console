using System;
using System.Linq;
using UniversityAcademicManagementSystem_Console;
using UniversityAcademicManagementSystem_Console.Models;
using UniversityAcademicManagementSystem_Console.Repositories;

namespace UniversityAcademicManagementSystem_Console.Services
{
	public class StudentService
	{
		private readonly StudentRepository _studentRepo;
		private readonly EnrollmentRepository _enrollmentRepo;
		private readonly GradeRepository _gradeRepo;
		private readonly AcademicRecordRepository _recordRepo;
		private readonly CourseRepository _courseRepo;

		public StudentService(StudentRepository studentRepo,
							  EnrollmentRepository enrollmentRepo,
							  GradeRepository gradeRepo,
							  AcademicRecordRepository recordRepo,
							  CourseRepository courseRepo)
		{
			_studentRepo = studentRepo;
			_enrollmentRepo = enrollmentRepo;
			_gradeRepo = gradeRepo;
			_recordRepo = recordRepo;
			_courseRepo = courseRepo;
		}

		// 1. Register Student
		public void RegisterStudent(Student student)
		{
			_studentRepo.Add(student);
			Console.WriteLine("Student registered successfully!");
		}

		// 2. Update Profile
		public void UpdateProfile(Student student)
		{
			var existing = _studentRepo.GetById(student.StudentId);
			if (existing == null)
			{
				Console.WriteLine("Student not found. Profile update cancelled.");
				return;
			}

			existing.Name = student.Name;
			existing.Email = student.Email;
			existing.Department = student.Department;
			existing.ContactNumber = student.ContactNumber;
			existing.EnrollmentYear = student.EnrollmentYear;

			_studentRepo.Update(existing);
			Console.WriteLine("Profile updated successfully.");
		}


		// 3. List My Courses
		public void ListMyCourses(int studentId, string semester)
		{
			var courses = _courseRepo.GetBySemester(semester)
				.Where(c => c.Enrollments != null &&
							c.Enrollments.Any(e => e.StudentId == studentId &&
												   e.EnrollmentStatus == EnrollmentStatus.ENROLLED));

			if (!courses.Any())
			{
				Console.WriteLine($"No courses found for StudentId {studentId} in {semester} semester.");
				return;
			}

			Console.WriteLine($"--- My Courses ({semester}) ---");
			foreach (var c in courses)
			{
				Console.WriteLine($"CourseId: {c.CourseId} | {c.CourseName} | Semester: {c.SemesterOffered}");
			}
			Console.WriteLine("-------------------------------");
		}




		// 4. Enroll in Course
		public void EnrollInCourse(int studentId, int courseId)
		{
			var course = _courseRepo.GetById(courseId);
			if (course == null)
			{
				Console.WriteLine($"Course with ID {courseId} does not exist. Enrollment failed.");
				return;
			}

			var existing = _enrollmentRepo.GetAll()
				.FirstOrDefault(e => e.StudentId == studentId &&
									 e.CourseId == courseId &&
									 e.EnrollmentStatus == EnrollmentStatus.ENROLLED);

			if (existing != null)
			{
				Console.WriteLine("Already enrolled in this course.");
				return;
			}

			var enrollment = new Enrollment
			{
				StudentId = studentId,
				CourseId = courseId,
				EnrollmentStatus = EnrollmentStatus.ENROLLED
			};

			_enrollmentRepo.Add(enrollment);
			Console.WriteLine($"Student {studentId} enrolled in course {course.CourseName} successfully!");
		}


		// 5. Drop Course
		public void DropCourse(int studentId, int courseId)
		{
			var enrollment = _enrollmentRepo.GetAll()
				.FirstOrDefault(e => e.StudentId == studentId && e.CourseId == courseId && e.EnrollmentStatus == EnrollmentStatus.ENROLLED);

			if (enrollment != null)
			{
				enrollment.EnrollmentStatus = EnrollmentStatus.DROPPED;
				_enrollmentRepo.Update(enrollment);
				Console.WriteLine("Course dropped successfully!");
			}
			else
			{
				Console.WriteLine("Enrollment not found or already dropped.");
			}
		}

		// 6. View Grades
		public void ViewGrades(int studentId)
		{
			var grades = _gradeRepo.GetAll().Where(g => g.StudentId == studentId);

			Console.WriteLine("--- My Grades ---");
			foreach (var g in grades)
			{
				Console.WriteLine($"CourseId: {g.CourseId} | Grade: {g.GradeValue} | Remarks: {g.Remarks}");
			}
			Console.WriteLine("-----------------");
		}

		// 7. Generate Transcript
		public void GenerateTranscript(int studentId)
		{
			var records = _recordRepo.GetAll().Where(r => r.StudentId == studentId);

			Console.WriteLine("--- Transcript ---");
			foreach (var r in records)
			{
				Console.WriteLine($"CourseId: {r.CourseId} | Grade: {r.Grade} | Semester: {r.Semester}");
			}
			Console.WriteLine("------------------");
		}
	}
}
