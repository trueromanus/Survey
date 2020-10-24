using System;
using System.Collections.Generic;

namespace Survey.Model.Entities
{
	public class Answer
	{

		public int Id { get; set; }

		public int? IntegerValue { get; set; }

		public string StringValue { get; set; }

		public bool? BoolValue { get; set; }

		public DateTime? DateValue { get; set; }

		public int QuestionaryId { get; set; }

		public ValueType ValueType { get; set; }

		public Questionary Questionary { get; set; }

		public ICollection<AnswerQuestion> AnswerQuestions { get; set; }

	}
}
