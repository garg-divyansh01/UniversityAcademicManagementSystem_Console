using UniversityAcademicManagementSystem_Console.Models;

namespace UniversityAcademicManagementSystem_Console.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Student GetStudentDetails(int id);
        void RegisterStudent(Student student);
        void UpdateProfile(Student student);
    }
}