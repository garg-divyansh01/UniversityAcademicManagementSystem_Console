using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityAcademicManagementSystem_Console.Models
{
	public class Grade
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int GradeId { get; set; }

		[ForeignKey("Student")]
		[Required]
		public int StudentId { get; set; }

		[ForeignKey("Course")]
		[Required]
		public int CourseId { get; set; }

		[Required]
		[StringLength(5)]
		public string GradeValue { get; set; }

		[StringLength(255)]
		public string Remarks { get; set; }

		public Student Student { get; set; }
		public Course Course { get; set; }
	}
}
