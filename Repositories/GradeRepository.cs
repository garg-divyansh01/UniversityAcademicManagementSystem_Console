using System;
using System.Collections.Generic;
using System.Text;
using UniversityAcademicManagementSystem_Console.Data;
using UniversityAcademicManagementSystem_Console.Models;

namespace UniversityAcademicManagementSystem_Console.Repositories
{
	public class GradeRepository : IGenericRepository<Grade>
	{
		private readonly UniversityDbContext _context;

		public GradeRepository(UniversityDbContext context)
		{
			_context = context;
		}

		public void Add(Grade entity)
		{
			try
			{
				_context.Grades.Add(entity);
				_context.SaveChanges();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error adding grade: {ex.Message}");
			}
		}

		public void Update(Grade entity)
		{
			try
			{
				_context.Grades.Update(entity);
				_context.SaveChanges();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error updating grade: {ex.Message}");
			}
		}

		public void Delete(int id)
		{
			try
			{
				var grade = _context.Grades.Find(id);
				if (grade != null)
				{
					_context.Grades.Remove(grade);
					_context.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error deleting grade: {ex.Message}");
			}
		}

		public Grade GetById(int id)
		{
			try { return _context.Grades.Find(id); }
			catch (Exception ex) { Console.WriteLine($"Error fetching grade: {ex.Message}"); return null; }
		}

		public IEnumerable<Grade> GetAll()
		{
			try { return _context.Grades.ToList(); }
			catch (Exception ex) { Console.WriteLine($"Error fetching grades: {ex.Message}"); return new List<Grade>(); }
		}
	}

}
