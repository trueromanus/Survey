using Microsoft.AspNetCore.Mvc;
using Survey.BL.Services;
using Survey.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Survey.Controllers
{

	/// <summary>
	/// Контроллер вопросов.
	/// </summary>
	[Route ( "api/questions" )]
	[ApiController]
	public class QuestionController : ControllerBase
	{
		private readonly IQuestionService _questionService;

		public QuestionController ( IQuestionService questionService ) => _questionService = questionService ?? throw new ArgumentNullException ( nameof ( questionService ) );

		/// <summary>
		/// Получить вопросы.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route ( "list" )]
		public async Task<IEnumerable<QuestionModel>> GetQuestionsAsync () => await _questionService.GetQuestions();

	}

}
