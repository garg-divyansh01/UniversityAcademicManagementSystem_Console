using UniversityAcademicManagementSystem_Console.Models;

namespace UniversityAcademicManagementSystem_Console.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        void AddCourse(Course course);
        Course GetCourseDetails(int id);
        IEnumerable<Course> ListCoursesBySemester(string semester);
        void UpdateCourse(Course course);
    }
}