using UniversityAcademicManagementSystem_Console.Models;

namespace UniversityAcademicManagementSystem_Console.Repositories.Interfaces
{
    public interface ITranscriptRepository
    {
        void AddAcademicRecord(AcademicRecord record);
        double GenerateTranscript(int studentId);
        IEnumerable<AcademicRecord> GetAcademicRecord(int studentId);
        void UpdateAcademicRecord(int studentId, int courseId, string newGrade);
    }
}