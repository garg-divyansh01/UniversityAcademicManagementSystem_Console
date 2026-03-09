using System.Collections.Generic;
using System.Linq;
using UniversityAcademicManagementSystem_Console.Data;
using UniversityAcademicManagementSystem_Console.Models;
using UniversityAcademicManagementSystem_Console.Repositories;

namespace UniversityAcademicManagementSystem_Console.Repositories
{
	public class StudentRepository : IGenericRepository<Student>
	{
		private readonly UniversityDbContext _context;

		public StudentRepository(UniversityDbContext context)
		{
			_context = context;
		}

		public void Add(Student entity)
		{
			try
			{
				_context.Students.Add(entity);
				_context.SaveChanges();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error adding student: {ex.Message}");
			}
		}

		public void Update(Student entity)
		{
			try
			{
				_context.Students.Update(entity);
				_context.SaveChanges();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error updating student: {ex.Message}");
			}
		}

		public void Delete(int id)
		{
			try
			{
				var student = _context.Students.Find(id);
				if (student != null)
				{
					_context.Students.Remove(student);
					_context.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error deleting student: {ex.Message}");
			}
		}

		public Student GetById(int id)
		{
			try { return _context.Students.Find(id); }
			catch (Exception ex) { Console.WriteLine($"Error fetching student: {ex.Message}"); return null; }
		}

		public IEnumerable<Student> GetAll()
		{
			try { return _context.Students.ToList(); }
			catch (Exception ex) { Console.WriteLine($"Error fetching students: {ex.Message}"); return new List<Student>(); }
		}
	}

}
