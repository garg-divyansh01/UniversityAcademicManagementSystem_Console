using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using UniversityAcademicManagementSystem_Console.Models;

namespace UniversityAcademicManagementSystem_Console.Data
{
	public class UniversityDbContext : DbContext
	{

		public UniversityDbContext() { }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			string conString = ConfigurationManager.ConnectionStrings["sqlcon"].ConnectionString;
			optionsBuilder.UseSqlServer(conString);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Enrollment>()
				.Property(e => e.EnrollmentStatus)
				.HasConversion<string>()
				.HasMaxLength(20);
		}


		public DbSet<Student> Students { get; set; }
		public DbSet<Course> Courses { get; set; }
		public DbSet<Enrollment> Enrollments { get; set; }
		public DbSet<Grade> Grades { get; set; }
		public DbSet<AcademicRecord> AcademicRecords { get; set; }
		
	}
}
