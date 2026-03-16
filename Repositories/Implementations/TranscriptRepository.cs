using System;
using System.Collections.Generic;
using System.Text;
using UniversityAcademicManagementSystem_Console.Data;
using UniversityAcademicManagementSystem_Console.Models;
using UniversityAcademicManagementSystem_Console.Repositories.Interfaces;

namespace UniversityAcademicManagementSystem_Console.Repositories.Implementations
{
    public class TranscriptRepository : ITranscriptRepository
    {
        private readonly UniversityDbContext _context;

        public TranscriptRepository(UniversityDbContext context)
        {
            _context = context;
        }

        public IEnumerable<AcademicRecord> GetAcademicRecord(int studentId)
        {
            return _context.AcademicRecords.Where(r => r.StudentId == studentId).ToList();
        }

        public double GenerateTranscript(int studentId)
        {
            var records = GetAcademicRecord(studentId);
            if (!records.Any())
                throw new InvalidOperationException("No academic records found for transcript.");

            var gpa = records.Select(r => ConvertGradeToPoints(r.Grade)).Average();
            return gpa;
        }

        private double ConvertGradeToPoints(string grade)
        {
            return grade switch
            {
                "A" => 4.0,
                "B" => 3.0,
                "C" => 2.0,
                "D" => 1.0,
                "F" => 0.0,
                _ => throw new ArgumentException($"Invalid grade value: {grade}")
            };
        }
    }
}
