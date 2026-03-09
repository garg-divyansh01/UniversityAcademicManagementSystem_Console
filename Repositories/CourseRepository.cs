using System;
using System.Collections.Generic;
using System.Text;
using UniversityAcademicManagementSystem_Console.Data;
using UniversityAcademicManagementSystem_Console.Models;

namespace UniversityAcademicManagementSystem_Console.Repositories
{
	public class CourseRepository : IGenericRepository<Course>
	{
		private readonly UniversityDbContext _context;

		public CourseRepository(UniversityDbContext context)
		{
			_context = context;
		}

		public void Add(Course entity)
		{
			try
			{
				_context.Courses.Add(entity);
				_context.SaveChanges();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error adding course: {ex.Message}");
			}
		}

		public void Update(Course entity)
		{
			try
			{
				_context.Courses.Update(entity);
				_context.SaveChanges();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error updating course: {ex.Message}");
			}
		}

		public void Delete(int id)
		{
			try
			{
				var course = _context.Courses.Find(id);
				if (course != null)
				{
					_context.Courses.Remove(course);
					_context.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error deleting course: {ex.Message}");
			}
		}

		public Course GetById(int id)
		{
			try { return _context.Courses.Find(id); }
			catch (Exception ex) { Console.WriteLine($"Error fetching course: {ex.Message}"); return null; }
		}

		public IEnumerable<Course> GetAll()
		{
			try { return _context.Courses.ToList(); }
			catch (Exception ex) { Console.WriteLine($"Error fetching courses: {ex.Message}"); return new List<Course>(); }
		}

		public IEnumerable<Course> GetBySemester(string semester)
		{
			try
			{
				return _context.Courses
							   .Where(c => !string.IsNullOrEmpty(c.SemesterOffered) &&
										   c.SemesterOffered.ToLower() == semester.ToLower())
							   .ToList();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error fetching courses by semester: {ex.Message}");
				return new List<Course>();
			}
		}
	}

}
