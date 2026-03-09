using System;
using UniversityAcademicManagementSystem_Console.Data;
using UniversityAcademicManagementSystem_Console.Models;
using UniversityAcademicManagementSystem_Console.Repositories;
using UniversityAcademicManagementSystem_Console.Services;

namespace UniversityAcademicManagementSystem_Console
{
	class Program
	{
		static void Main(string[] args)
		{
			using var context = new UniversityDbContext();

			var studentService = new StudentService(
				new StudentRepository(context),
				new EnrollmentRepository(context),
				new GradeRepository(context),
				new AcademicRecordRepository(context),
				new CourseRepository(context));

			var facultyService = new FacultyService(
				new CourseRepository(context),
				new EnrollmentRepository(context),
				new GradeRepository(context));

			var registrarService = new RegistrarService(
				new CourseRepository(context),
				new EnrollmentRepository(context));

			var adminService = new AdminService(
				new StudentRepository(context),
				new CourseRepository(context));

			bool exit = false;
			while (!exit)
			{
				Console.WriteLine("===============================");
				Console.WriteLine(" University Enrollment System");
				Console.WriteLine("===============================");
				Console.WriteLine("Select your role:");
				Console.WriteLine("1. Student");
				Console.WriteLine("2. Faculty");
				Console.WriteLine("3. Registrar");
				Console.WriteLine("4. Admin");
				Console.WriteLine("0. Exit");
				Console.WriteLine("===============================");

				var choice = Console.ReadLine();
				switch (choice)
				{
					case "1": ShowStudentMenu(studentService); break;
					case "2": ShowFacultyMenu(facultyService); break;
					case "3": ShowRegistrarMenu(registrarService); break;
					case "4": ShowAdminMenu(adminService); break;
					case "0":
						Console.WriteLine("Thank You!");
						exit = true; break;
					default: Console.WriteLine("Invalid choice."); break;
				}
			}
		}

		// --- Student Menu ---
		static void ShowStudentMenu(StudentService studentService)
		{
			bool back = false;
			while (!back)
			{
				Console.WriteLine("--- Student Menu ---");
				Console.WriteLine("1. Register Student");
				Console.WriteLine("2. Update Profile");
				Console.WriteLine("3. List My Courses");
				Console.WriteLine("4. Enroll in Course");
				Console.WriteLine("5. Drop Course");
				Console.WriteLine("6. View Grades");
				Console.WriteLine("7. Generate Transcript");
				Console.WriteLine("0. Back to Role Selection");

				var choice = Console.ReadLine();
				switch (choice)
				{
					case "1":
						Console.Write("Name: "); var name = Console.ReadLine();
						Console.Write("Email: "); var email = Console.ReadLine();
						Console.Write("Department: "); var dept = Console.ReadLine();
						Console.Write("Contact: "); var contact = Console.ReadLine();
						Console.Write("Enrollment Year: "); var year = int.Parse(Console.ReadLine());
						var student = new Student { Name = name, Email = email, Department = dept, ContactNumber = contact, EnrollmentYear = year };
						studentService.RegisterStudent(student);
						break;
					case "2":
						Console.Write("StudentId: "); var sid = int.Parse(Console.ReadLine());
						Console.Write("New Name: "); name = Console.ReadLine();
						Console.Write("New Email: "); email = Console.ReadLine();
						Console.Write("New Department: "); dept = Console.ReadLine();
						Console.Write("New Contact: "); contact = Console.ReadLine();
						Console.Write("Enrollment Year: "); year = int.Parse(Console.ReadLine());
						var updated = new Student { StudentId = sid, Name = name, Email = email, Department = dept, ContactNumber = contact, EnrollmentYear = year };
						studentService.UpdateProfile(updated);
						break;
					case "3":
						Console.Write("StudentId: "); sid = int.Parse(Console.ReadLine());
						Console.Write("Semester: "); var sem = Console.ReadLine();
						studentService.ListMyCourses(sid, sem);
						break;
					case "4":
						Console.Write("StudentId: "); sid = int.Parse(Console.ReadLine());
						Console.Write("CourseId: "); var cid = int.Parse(Console.ReadLine());
						studentService.EnrollInCourse(sid, cid);
						break;
					case "5":
						Console.Write("StudentId: "); sid = int.Parse(Console.ReadLine());
						Console.Write("CourseId: "); cid = int.Parse(Console.ReadLine());
						studentService.DropCourse(sid, cid);
						break;
					case "6":
						Console.Write("StudentId: "); sid = int.Parse(Console.ReadLine());
						studentService.ViewGrades(sid);
						break;
					case "7":
						Console.Write("StudentId: "); sid = int.Parse(Console.ReadLine());
						studentService.GenerateTranscript(sid);
						break;
					case "0": back = true; break;
					default: Console.WriteLine("Invalid choice."); break;
				}
			}
		}

		// --- Faculty Menu ---
		static void ShowFacultyMenu(FacultyService facultyService)
		{
			bool back = false;
			while (!back)
			{
				Console.WriteLine("--- Faculty Menu ---");
				Console.WriteLine("1. List My Courses");
				Console.WriteLine("2. View Enrolled Students");
				Console.WriteLine("3. Submit Grade");
				Console.WriteLine("4. Update Grade");
				Console.WriteLine("5. View Grades by Course");
				Console.WriteLine("0. Back to Role Selection");

				var choice = Console.ReadLine();
				switch (choice)
				{
					case "1":
						Console.Write("Department: "); var dept = Console.ReadLine();
						Console.Write("Semester: "); var sem = Console.ReadLine();
						facultyService.ListMyCourses(dept, sem);
						break;
					case "2":
						Console.Write("CourseId: "); var cid = int.Parse(Console.ReadLine());
						facultyService.ViewEnrolledStudents(cid);
						break;
					case "3":
						Console.Write("StudentId: "); var sid = int.Parse(Console.ReadLine());
						Console.Write("CourseId: "); cid = int.Parse(Console.ReadLine());
						Console.Write("Grade: "); var gradeVal = Console.ReadLine();
						Console.Write("Remarks: "); var remarks = Console.ReadLine();
						var grade = new Grade { StudentId = sid, CourseId = cid, GradeValue = gradeVal, Remarks = remarks };
						facultyService.SubmitGrade(grade);
						break;
					case "4":
						Console.Write("GradeId: "); var gid = int.Parse(Console.ReadLine());
						Console.Write("New Grade: "); gradeVal = Console.ReadLine();
						Console.Write("Remarks: "); remarks = Console.ReadLine();
						var updated = new Grade { GradeId = gid, GradeValue = gradeVal, Remarks = remarks };
						facultyService.UpdateGrade(updated);
						break;
					case "5":
						Console.Write("CourseId: "); cid = int.Parse(Console.ReadLine());
						facultyService.ViewGradesByCourse(cid);
						break;
					case "0": back = true; break;
					default: Console.WriteLine("Invalid choice."); break;
				}
			}
		}

		// --- Registrar Menu ---
		static void ShowRegistrarMenu(RegistrarService registrarService)
		{
			bool back = false;
			while (!back)
			{
				Console.WriteLine("--- Registrar Menu ---");
				Console.WriteLine("1. Add Course");
				Console.WriteLine("2. Update Course");
				Console.WriteLine("3. List Courses");
				Console.WriteLine("4. Assign Course to Semester");
				Console.WriteLine("5. View Enrollment Records");
				Console.WriteLine("0. Back to Role Selection");

				var choice = Console.ReadLine();
				switch (choice)
				{
					case "1":
						Console.Write("Course Name: "); var cname = Console.ReadLine();
						Console.Write("Credits: "); var credits = int.Parse(Console.ReadLine());
						Console.Write("Department: "); var dept = Console.ReadLine();
						Console.Write("Semester: "); var sem = Console.ReadLine();
						var course = new Course { CourseName = cname, Credits = credits, Department = dept, SemesterOffered = sem };
						registrarService.AddCourse(course);
						break;
					case "2":
						Console.Write("CourseId: "); var cid = int.Parse(Console.ReadLine());
						Console.Write("Course Name: "); cname = Console.ReadLine();
						Console.Write("Credits: "); credits = int.Parse(Console.ReadLine());
						Console.Write("Department: "); dept = Console.ReadLine();
						Console.Write("Semester: "); sem = Console.ReadLine();
						var updated = new Course { CourseId = cid, CourseName = cname, Credits = credits, Department = dept, SemesterOffered = sem };
						registrarService.UpdateCourse(updated);
						break;
					case "3":
						registrarService.ListCourses();
						break;
					case "4":
						Console.Write("CourseId: "); cid = int.Parse(Console.ReadLine());
						Console.Write("Semester: "); sem = Console.ReadLine();
						registrarService.AssignCourseToSemester(cid, sem);
						break;
					case "5":
						registrarService.ViewEnrollmentRecords();
						break;
					case "0": back = true; break;
					default: Console.WriteLine("Invalid choice."); break;
				}
			}
		}

		// --- Admin Menu ---
		static void ShowAdminMenu(AdminService adminService)
		{
			bool back = false;
			while (!back)
			{
				Console.WriteLine("--- Admin Menu ---");
				Console.WriteLine("1. View All Students");
				Console.WriteLine("2. Remove Student");
				Console.WriteLine("3. View All Courses");
				Console.WriteLine("4. Remove Course");
				Console.WriteLine("0. Back to Role Selection");

				var choice = Console.ReadLine();
				switch (choice)
				{
					case "1":
						adminService.ViewAllStudents();
						break;
					case "2":
						Console.Write("Enter StudentId to remove: ");
						var sid = int.Parse(Console.ReadLine());
						adminService.RemoveStudent(sid);
						break;
					case "3":
						adminService.ViewAllCourses();
						break;
					case "4":
						Console.Write("Enter CourseId to remove: ");
						var cid = int.Parse(Console.ReadLine());
						adminService.RemoveCourse(cid);
						break;
					case "0":
						back = true;
						break;
					default:
						Console.WriteLine("Invalid choice.");
						break;
				}
			}
		}
	}
}