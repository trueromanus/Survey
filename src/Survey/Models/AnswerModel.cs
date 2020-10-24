using Survey.Model.Entities;

namespace Survey.Models
{
	public class AnswerModel
	{

		public object Value { get; set; }

		public int QuestionId { get; set; }

		public ValueType ValueType { get; set; }

	}
}
