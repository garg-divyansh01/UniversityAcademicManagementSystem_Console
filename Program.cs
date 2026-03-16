using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
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
            Console.WriteLine("\n-------------------------------");
            Console.WriteLine(" University Enrollment System ");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Select Role:");
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
                case "2": FacultyMenu(gradeRepo, transcriptRepo, courseRepo); break;
                case "3": RegistrarMenu(courseRepo); break;
                case "4": AdminMenu(studentRepo, courseRepo, enrollmentRepo, gradeRepo, transcriptRepo); break;
                case "0": exit = true; break;
                default: Console.WriteLine("Invalid choice."); break;
            }
        }
    }

    // --- STUDENT MENU  ---
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
                    Console.Write("Department: "); var dept = Console.ReadLine().ToUpper();
                    Console.Write("Contact: "); var contact = Console.ReadLine();
                    Console.Write("Year: "); var year = int.Parse(Console.ReadLine());
                    studentRepo.RegisterStudent(new Student { Name = name, Email = email, Department = dept, ContactNumber = contact, EnrollmentYear = year });
                    Console.WriteLine("Success: Student registered.");
                    break;
                case "2":
                    Console.Write("Student ID: "); var id = int.Parse(Console.ReadLine());
                    var existing = studentRepo.GetStudentDetails(id);
                    if (existing != null)
                    {
                        Console.Write("New Name: "); existing.Name = Console.ReadLine();
                        Console.Write("New Email: "); existing.Email = Console.ReadLine();
                        Console.Write("New Department: "); existing.Department = Console.ReadLine().ToUpper();
                        Console.Write("New Contact: "); existing.ContactNumber = Console.ReadLine();
                        studentRepo.UpdateProfile(existing);
                        Console.WriteLine("Success: Profile updated.");
                    }
                    else { Console.WriteLine("Error: Student not found."); }
                    break;
                case "3":
                    Console.Write("Student ID: "); var sid = int.Parse(Console.ReadLine());
                    var student = studentRepo.GetStudentDetails(sid);
                    if (student != null)
                        Console.WriteLine($"Details - Name: {student.Name}, Email: {student.Email}, Dept: {student.Department}");
                    else Console.WriteLine("Error: Student not found.");
                    break;
                case "4":
                    Console.Write("Student ID: "); var stid = int.Parse(Console.ReadLine());
                    Console.Write("Course ID: "); var cid = int.Parse(Console.ReadLine());
                    enrollmentRepo.EnrollCourse(stid, cid);
                    Console.WriteLine("Success: Enrolled in course.");
                    break;
                case "5":
                    Console.Write("Student ID: "); var dsid = int.Parse(Console.ReadLine());
                    Console.Write("Course ID: "); var dcid = int.Parse(Console.ReadLine());
                    enrollmentRepo.DropCourse(dsid, dcid);
                    Console.WriteLine("Success: Dropped course.");
                    break;
                case "6":
                    Console.Write("Student ID: "); var esid = int.Parse(Console.ReadLine());
                    var courses = enrollmentRepo.GetEnrolledCourses(esid);
                    foreach (var e in courses) Console.WriteLine($"CourseId: {e.CourseId}, Status: {e.EnrollmentStatus}");
                    break;
                case "7":
                    Console.Write("Student ID: "); var tsid = int.Parse(Console.ReadLine());
                    var gpa = transcriptRepo.GenerateTranscript(tsid);
                    Console.WriteLine($"Final GPA Result: {gpa:F2}");
                    break;
                default: Console.WriteLine("Invalid choice."); break;
            }
        }
        catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
    }

    // --- FACULTY MENU ---
    static void FacultyMenu(IGradeRepository gradeRepo, ITranscriptRepository transcriptRepo, ICourseRepository courseRepo)
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
                    Console.Write("Student ID: "); int sid = int.Parse(Console.ReadLine());
                    Console.Write("Course ID: "); int cid = int.Parse(Console.ReadLine());
                    Console.Write("Grade (A/B/C): "); string gradeVal = Console.ReadLine().ToUpper();
                    Console.Write("Remarks: "); string rem = Console.ReadLine();
                    var courseInfo = courseRepo.GetCourseDetails(cid);

                    if (courseInfo != null)
                    {
                    
                        gradeRepo.SubmitGrade(new Grade
                        {
                            StudentId = sid,
                            CourseId = cid,
                            GradeValue = gradeVal,
                            Remarks = rem
                        });

                        transcriptRepo.AddAcademicRecord(new AcademicRecord
                        {
                            StudentId = sid,
                            CourseId = cid,
                            Grade = gradeVal,
                            Semester = courseInfo.SemesterOffered
                        });

                        Console.WriteLine("Success: Grade submitted and Academic Record synchronized.");
                    }
                    else
                    {
                        Console.WriteLine("Error: Invalid Course ID. Could not find course semester.");
                    }
                    break;
                case "2":
                    Console.Write("Enter Grade ID to update: ");
                    int gid = int.Parse(Console.ReadLine());

                    var existingGrade = gradeRepo.GetGradeById(gid);

                    if (existingGrade != null)
                    {
                        Console.Write("New Grade Value: ");
                        string updatedGrade = Console.ReadLine().ToUpper();

                        Console.Write("New Remarks: ");
                        string updatedRemarks = Console.ReadLine();
                        gradeRepo.UpdateGrade(new Grade
                        {
                            GradeId = gid,
                            GradeValue = updatedGrade,
                            Remarks = updatedRemarks
                        });
                        transcriptRepo.UpdateAcademicRecord(existingGrade.StudentId, existingGrade.CourseId, updatedGrade);

                        Console.WriteLine("Success: Grade updated in both Grading and Academic Records.");
                    }
                    else
                    {
                        Console.WriteLine("Error: Grade ID not found.");
                    }
                    break;
                case "3":
                    Console.Write("Course ID: "); var courseId = int.Parse(Console.ReadLine());
                    var grades = gradeRepo.GetGradesByCourse(courseId);
                    foreach (var g in grades) Console.WriteLine($"StudentId: {g.StudentId}, Grade: {g.GradeValue}, Remarks: {g.Remarks}");
                    break;
                default: Console.WriteLine("Invalid choice."); break;
            }
        }
        catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
    }

    // --- REGISTRAR MENU ---
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
                    Console.Write("Department: "); var dept = Console.ReadLine().ToUpper();
                    Console.Write("Semester: "); var semOffered = Console.ReadLine().ToUpper();
                    courseRepo.AddCourse(new Course { CourseName = cname, Credits = credits, Department = dept, SemesterOffered = semOffered });
                    Console.WriteLine("Success: Course added.");
                    break;
                case "2":
                    Console.Write("Course ID to update: "); var cid = int.Parse(Console.ReadLine());
                    var existingC = courseRepo.GetCourseDetails(cid);
                    if (existingC != null)
                    {
                        Console.Write("New Course Name: "); existingC.CourseName = Console.ReadLine();
                        Console.Write("New Credits: "); existingC.Credits = int.Parse(Console.ReadLine());
                        Console.Write("New Department: "); existingC.Department = Console.ReadLine().ToUpper();
                        Console.Write("New Semester: "); existingC.SemesterOffered = Console.ReadLine().ToUpper();
                        courseRepo.UpdateCourse(existingC);
                        Console.WriteLine("Success: Course updated.");
                    }
                    else { Console.WriteLine("Error: Course not found."); }
                    break;
                case "3":
                    Console.Write("Course ID: "); var id = int.Parse(Console.ReadLine());
                    var course = courseRepo.GetCourseDetails(id);
                    if (course != null)
                        Console.WriteLine($"Details - Name: {course.CourseName}, Credits: {course.Credits}, Dept: {course.Department}, Sem: {course.SemesterOffered}");
                    else Console.WriteLine("Error: Course not found.");
                    break;
                case "4":
                    Console.Write("Semester Name: "); var sem = Console.ReadLine().ToUpper();
                    var courses = courseRepo.ListCoursesBySemester(sem);
                    foreach (var c in courses) Console.WriteLine($"Course: {c.CourseName} (ID: {c.CourseId}, Credits: {c.Credits})");
                    break;
                default: Console.WriteLine("Invalid choice."); break;
            }
        }
        catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
    }

    // --- ADMIN MENU ---
    static void AdminMenu(IStudentRepository studentRepo, ICourseRepository courseRepo, IEnrollmentRepository enrollmentRepo, IGradeRepository gradeRepo, ITranscriptRepository transcriptRepo)
    {
        Console.WriteLine("\n--- Admin Menu ---");
        Console.WriteLine("Admins can access all Student, Course, Enrollment, Grade, and Transcript operations.");
        Console.WriteLine("For demo, try StudentMenu, FacultyMenu, or RegistrarMenu again.");
    }
}