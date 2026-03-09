using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityAcademicManagementSystem_Console.Models
{
	public class Course
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CourseId { get; set; }

		[Required]
		[StringLength(100)]
		public string CourseName { get; set; }

		[Required]
		[Range(1, 10)]
		public int Credits { get; set; }

		[Required]
		[StringLength(100)]
		public string Department { get; set; }

		[Required]
		[StringLength(20)]
		public string SemesterOffered { get; set; }

		public ICollection<Enrollment> Enrollments { get; set; }
		public ICollection<Grade> Grades { get; set; }
		public ICollection<AcademicRecord> AcademicRecords { get; set; }
	}
}
