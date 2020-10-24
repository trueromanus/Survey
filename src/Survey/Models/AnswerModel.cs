using Survey.Model.Entities;
using System.ComponentModel.DataAnnotations;

namespace Survey.Models
{
	public class AnswerModel
	{

		[Required]
		public object Value { get; set; }

		[Required]
		public int QuestionId { get; set; }

		[Required]
		public ValueType ValueType { get; set; }

	}
}
