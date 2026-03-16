using UniversityAcademicManagementSystem_Console.Models;

namespace UniversityAcademicManagementSystem_Console.Repositories.Interfaces
{
    public interface IGradeRepository
    {
        Grade GetGradeById(int gid);
        IEnumerable<Grade> GetGradesByCourse(int courseId);
        void SubmitGrade(Grade grade);
        void UpdateGrade(Grade grade);
    }
}