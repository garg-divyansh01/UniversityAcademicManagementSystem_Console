using UniversityAcademicManagementSystem_Console.Models;

namespace UniversityAcademicManagementSystem_Console.Repositories.Interfaces
{
    public interface ITranscriptRepository
    {
        double GenerateTranscript(int studentId);
        IEnumerable<AcademicRecord> GetAcademicRecord(int studentId);
    }
}