using Survey.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Survey.BL.Services
{
	public interface IQuestionService
	{

		/// <summary>
		/// Получить вопросы.
		/// </summary>
		/// <returns>Коллекция вопросов.</returns>
		Task<IEnumerable<QuestionModel>> GetQuestions ();

	}
}
