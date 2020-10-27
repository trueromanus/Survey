using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Survey.BL;
using Survey.Models;

namespace Survey.Controllers
{
	
	/// <summary>
	/// Контроллер ответов.
	/// </summary>
	[Route ( "api/answers" )]
	[ApiController]
	public class AnswerController : ControllerBase
	{
		private readonly IAnswerService _answerService;

		public AnswerController ( IAnswerService answerService ) => _answerService = answerService;

		/// <summary>
		/// Сохранить ответы.
		/// </summary>
		/// <param name="answers">Коллекция ответов.</param>
		[HttpPost]
		[Route ( "save" )]
		public async Task<bool> SaveAnswers ( [FromBody] IEnumerable<AnswerModel> answers ) {
			if ( !ModelState.IsValid ) return false;

			try {
				await _answerService.SaveAnswersAsync ( answers );
			} catch {
				return false;
			}

			return true;
		}

	}

}
