using System;
using System.Collections.Generic;
using System.Text;
using UniversityAcademicManagementSystem_Console.Data;
using UniversityAcademicManagementSystem_Console.Models;

namespace UniversityAcademicManagementSystem_Console.Repositories
{
	public class AcademicRecordRepository : IGenericRepository<AcademicRecord>
	{
		private readonly UniversityDbContext _context;

		public AcademicRecordRepository(UniversityDbContext context)
		{
			_context = context;
		}

		public void Add(AcademicRecord entity)
		{
			try
			{
				_context.AcademicRecords.Add(entity);
				_context.SaveChanges();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error adding academic record: {ex.Message}");
			}
		}

		public void Update(AcademicRecord entity)
		{
			try
			{
				_context.AcademicRecords.Update(entity);
				_context.SaveChanges();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error updating academic record: {ex.Message}");
			}
		}

		public void Delete(int id)
		{
			try
			{
				var record = _context.AcademicRecords.Find(id);
				if (record != null)
				{
					_context.AcademicRecords.Remove(record);
					_context.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error deleting academic record: {ex.Message}");
			}
		}

		public AcademicRecord GetById(int id)
		{
			try { return _context.AcademicRecords.Find(id); }
			catch (Exception ex) { Console.WriteLine($"Error fetching record: {ex.Message}"); return null; }
		}

		public IEnumerable<AcademicRecord> GetAll()
		{
			try { return _context.AcademicRecords.ToList(); }
			catch (Exception ex) { Console.WriteLine($"Error fetching records: {ex.Message}"); return new List<AcademicRecord>(); }
		}
	}

}
