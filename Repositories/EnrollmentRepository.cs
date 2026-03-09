using System;
using System.Collections.Generic;
using System.Text;
using UniversityAcademicManagementSystem_Console.Data;
using UniversityAcademicManagementSystem_Console.Models;

namespace UniversityAcademicManagementSystem_Console.Repositories
{
	public class EnrollmentRepository : IGenericRepository<Enrollment>
{
    private readonly UniversityDbContext _context;

    public EnrollmentRepository(UniversityDbContext context)
    {
        _context = context;
    }

    public void Add(Enrollment entity)
    {
        try
        {
            _context.Enrollments.Add(entity);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding enrollment: {ex.Message}");
        }
    }

    public void Update(Enrollment entity)
    {
        try
        {
            _context.Enrollments.Update(entity);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating enrollment: {ex.Message}");
        }
    }

    public void Delete(int id)
    {
        try
        {
            var enrollment = _context.Enrollments.Find(id);
            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);
                _context.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting enrollment: {ex.Message}");
        }
    }

    public Enrollment GetById(int id)
    {
        try { return _context.Enrollments.Find(id); }
        catch (Exception ex) { Console.WriteLine($"Error fetching enrollment: {ex.Message}"); return null; }
    }

    public IEnumerable<Enrollment> GetAll()
    {
        try { return _context.Enrollments.ToList(); }
        catch (Exception ex) { Console.WriteLine($"Error fetching enrollments: {ex.Message}"); return new List<Enrollment>(); }
    }
}

}
