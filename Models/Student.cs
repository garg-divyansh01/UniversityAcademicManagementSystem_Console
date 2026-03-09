using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityAcademicManagementSystem_Console.Models
{
	public class Student
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int StudentId { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		[Required]
		[StringLength(100)]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[StringLength(100)]
		public string Department { get; set; }

		[Required]
		[StringLength(20)]
		[Phone]
		public string ContactNumber { get; set; }

		[Required]
		public int EnrollmentYear { get; set; }

		public ICollection<Enrollment> Enrollments { get; set; }
		public ICollection<Grade> Grades { get; set; }
		public ICollection<AcademicRecord> AcademicRecords { get; set; }
	}
}
