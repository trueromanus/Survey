using Survey.Model.Entities;
using System.Collections.Generic;

namespace Survey.Common
{
	public class QuestionModel
	{

		public int Id { get; set; }

		public string Title { get; set; }

		public ValueType ValueType { get; set; }

		public ICollection<QuestionOptionModel> QuestionOptions { get; set; }

	}
}
