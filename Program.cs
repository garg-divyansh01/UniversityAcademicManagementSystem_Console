using Microsoft.EntityFrameworkCore;
using System;
using UniversityAcademicManagementSystem_Console.Data;
using UniversityAcademicManagementSystem_Console.Models;
using UniversityAcademicManagementSystem_Console.Repositories;
using UniversityAcademicManagementSystem_Console.Repositories.Implementations;
using UniversityAcademicManagementSystem_Console.Repositories.Interfaces;

class Program
{
    static void Main(string[] args)
    {
        using var context = new UniversityDbContext();

        // Initialize repositories
        IStudentRepository studentRepo = new StudentRepository(context);
        IGradeRepository gradeRepo = new GradeRepository(context);
        IEnrollmentRepository enrollmentRepo = new EnrollmentRepository(context);
        ICourseRepository courseRepo = new CourseRepository(context);
        ITranscriptRepository transcriptRepo = new TranscriptRepository(context);

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\nSelect Role:");
            Console.WriteLine("1. Student");
            Console.WriteLine("2. Faculty");
            Console.WriteLine("3. Registrar");
            Console.WriteLine("4. Admin");
            Console.WriteLine("0. Exit");
            Console.Write("Choice: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1": StudentMenu(studentRepo, enrollmentRepo, transcriptRepo); break;
                case "2": FacultyMenu(gradeRepo); break;
                case "3": RegistrarMenu(courseRepo); break;
                case "4": AdminMenu(studentRepo, courseRepo, enrollmentRepo, gradeRepo, transcriptRepo); break;
                case "0": exit = true; break;
                default: Console.WriteLine("Invalid choice."); break;
            }
        }
    }

    // Student Menu
    static void StudentMenu(IStudentRepository studentRepo, IEnrollmentRepository enrollmentRepo, ITranscriptRepository transcriptRepo)
    {
        Console.WriteLine("\n--- Student Menu ---");
        Console.WriteLine("1. Register Student");
        Console.WriteLine("2. Update Profile");
        Console.WriteLine("3. View My Details");
        Console.WriteLine("4. Enroll in Course");
        Console.WriteLine("5. Drop Course");
        Console.WriteLine("6. View Enrolled Courses");
        Console.WriteLine("7. View Transcript");
        Console.Write("Choice: ");
        var choice = Console.ReadLine();

        try
        {
            switch (choice)
            {
                case "1":
                    Console.Write("Name: "); var name = Console.ReadLine();
                    Console.Write("Email: "); var email = Console.ReadLine();
                    Console.Write("Department: "); var dept = Console.ReadLine();
                    Console.Write("Contact: "); var contact = Console.ReadLine();
                    Console.Write("Year: "); var year = int.Parse(Console.ReadLine());
                    studentRepo.RegisterStudent(new Student { Name = name, Email = email, Department = dept, ContactNumber = contact, EnrollmentYear = year });
                    Console.WriteLine("Student registered.");
                    break;
                case "2":
                    Console.Write("Student ID: "); var id = int.Parse(Console.ReadLine());
                    Console.Write("New Name: "); var newName = Console.ReadLine();
                    studentRepo.UpdateProfile(new Student { StudentId = id, Name = newName });
                    Console.WriteLine("Profile updated.");
                    break;
                case "3":
                    Console.Write("Student ID: "); var sid = int.Parse(Console.ReadLine());
                    var student = studentRepo.GetStudentDetails(sid);
                    Console.WriteLine($"Student: {student.Name}, {student.Email}, {student.Department}");
                    break;
                case "4":
                    Console.Write("Student ID: "); var stid = int.Parse(Console.ReadLine());
                    Console.Write("Course ID: "); var cid = int.Parse(Console.ReadLine());
                    enrollmentRepo.EnrollCourse(stid, cid);
                    Console.WriteLine("Enrolled in course.");
                    break;
                case "5":
                    Console.Write("Student ID: "); var dsid = int.Parse(Console.ReadLine());
                    Console.Write("Course ID: "); var dcid = int.Parse(Console.ReadLine());
                    enrollmentRepo.DropCourse(dsid, dcid);
                    Console.WriteLine("Dropped course.");
                    break;
                case "6":
                    Console.Write("Student ID: "); var esid = int.Parse(Console.ReadLine());
                    var courses = enrollmentRepo.GetEnrolledCourses(esid);
                    foreach (var e in courses) Console.WriteLine($"CourseId: {e.CourseId}, Status: {e.EnrollmentStatus}");
                    break;
                case "7":
                    Console.Write("Student ID: "); var tsid = int.Parse(Console.ReadLine());
                    var gpa = transcriptRepo.GenerateTranscript(tsid);
                    Console.WriteLine($"GPA: {gpa:F2}");
                    break;
                default: Console.WriteLine("Invalid choice."); break;
            }
        }
        catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
    }

    // Faculty Menu
    static void FacultyMenu(IGradeRepository gradeRepo)
    {
        Console.WriteLine("\n--- Faculty Menu ---");
        Console.WriteLine("1. Submit Grade");
        Console.WriteLine("2. Update Grade");
        Console.WriteLine("3. View Grades by Course");
        Console.Write("Choice: ");
        var choice = Console.ReadLine();

        try
        {
            switch (choice)
            {
                case "1":
                    Console.Write("Student ID: "); var sid = int.Parse(Console.ReadLine());
                    Console.Write("Course ID: "); var cid = int.Parse(Console.ReadLine());
                    Console.Write("Grade: "); var grade = Console.ReadLine();
                    gradeRepo.SubmitGrade(new Grade { StudentId = sid, CourseId = cid, GradeValue = grade });
                    Console.WriteLine("Grade submitted.");
                    break;
                case "2":
                    Console.Write("Grade ID: "); var gid = int.Parse(Console.ReadLine());
                    Console.Write("New Grade: "); var newGrade = Console.ReadLine();
                    gradeRepo.UpdateGrade(new Grade { GradeId = gid, GradeValue = newGrade });
                    Console.WriteLine("Grade updated.");
                    break;
                case "3":
                    Console.Write("Course ID: "); var courseId = int.Parse(Console.ReadLine());
                    var grades = gradeRepo.GetGradesByCourse(courseId);
                    foreach (var g in grades) Console.WriteLine($"StudentId: {g.StudentId}, Grade: {g.GradeValue}");
                    break;
                default: Console.WriteLine("Invalid choice."); break;
            }
        }
        catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
    }

    // Registrar Menu
    static void RegistrarMenu(ICourseRepository courseRepo)
    {
        Console.WriteLine("\n--- Registrar Menu ---");
        Console.WriteLine("1. Add Course");
        Console.WriteLine("2. Update Course");
        Console.WriteLine("3. View Course Details");
        Console.WriteLine("4. List Courses by Semester");
        Console.Write("Choice: ");
        var choice = Console.ReadLine();

        try
        {
            switch (choice)
            {
                case "1":
                    Console.Write("Course Name: "); var cname = Console.ReadLine();
                    Console.Write("Credits: "); var credits = int.Parse(Console.ReadLine());
                    courseRepo.AddCourse(new Course { CourseName = cname, Credits = credits });
                    Console.WriteLine("Course added.");
                    break;
                case "2":
                    Console.Write("Course ID: "); var cid = int.Parse(Console.ReadLine());
                    Console.Write("New Name: "); var newName = Console.ReadLine();
                    courseRepo.UpdateCourse(new Course { CourseId = cid, CourseName = newName });
                    Console.WriteLine("Course updated.");
                    break;
                case "3":
                    Console.Write("Course ID: "); var id = int.Parse(Console.ReadLine());
                    var course = courseRepo.GetCourseDetails(id);
                    Console.WriteLine($"Course: {course.CourseName}, Credits: {course.Credits}");
                    break;
                case "4":
                    Console.Write("Semester: "); var sem = Console.ReadLine();
                    var courses = courseRepo.ListCoursesBySemester(sem);
                    foreach (var c in courses) Console.WriteLine($"Course: {c.CourseName} ({c.Credits} credits)");
                    break;
                default: Console.WriteLine("Invalid choice."); break;
            }
        }
        catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
    }

    // Admin Menu
    static void AdminMenu(IStudentRepository studentRepo, ICourseRepository courseRepo, IEnrollmentRepository enrollmentRepo, IGradeRepository gradeRepo, ITranscriptRepository transcriptRepo)
    {
        Console.WriteLine("\n--- Admin Menu ---");
        Console.WriteLine("Admins can access all Student, Course, Enrollment, Grade, and Transcript operations.");
        Console.WriteLine("For demo, try StudentMenu, FacultyMenu, or RegistrarMenu again.");
    }
}