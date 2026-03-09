using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityAcademicManagementSystem_Console.Models
{
	public class Enrollment
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int EnrollmentId { get; set; }

		[ForeignKey("Student")]
		[Required]
		public int StudentId { get; set; }

		[ForeignKey("Course")]
		[Required]
		public int CourseId { get; set; }

		[Required]
		public EnrollmentStatus EnrollmentStatus { get; set; }

		public Student Student { get; set; }
		public Course Course { get; set; }
	}
}
