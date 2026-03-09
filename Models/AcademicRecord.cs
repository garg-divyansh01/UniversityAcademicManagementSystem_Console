using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityAcademicManagementSystem_Console.Models
{
	public class AcademicRecord
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int RecordId { get; set; }

		[ForeignKey("Student")]
		[Required]
		public int StudentId { get; set; }

		[ForeignKey("Course")]
		[Required]
		public int CourseId { get; set; }

		[Required]
		[StringLength(5)]
		public string Grade { get; set; }

		[Required]
		[StringLength(20)]
		public string Semester { get; set; }

		public Student Student { get; set; }
		public Course Course { get; set; }
	}
}
