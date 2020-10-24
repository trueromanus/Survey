using System.Collections.Generic;

namespace Survey.Model.Entities
{
	public class Question
	{

		public int Id { get; set; }

		public string Title { get; set; }

		public ValueType ValueType { get; set; }

		public ICollection<AnswerQuestion> AnswerQuestions { get; set; }

		public ICollection<QuestionOption> QuestionOptions { get; set; }

	}

}
