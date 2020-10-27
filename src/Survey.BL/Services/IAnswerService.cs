using Survey.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Survey.BL
{
	public interface IAnswerService
	{

		/// <summary>
		/// Сохранить ответы.
		/// </summary>
		/// <param name="answers">Ответы.</param>
		/// <returns>Результат выполнения.</returns>
		Task SaveAnswersAsync ( IEnumerable<AnswerModel> answers );

	}

}
