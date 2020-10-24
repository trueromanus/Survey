using Microsoft.AspNetCore.Mvc;
using Survey.Model.Entities;
using Survey.Model.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Survey.Controllers
{
	[Route ( "api/questions" )]
	[ApiController]
	public class QuestionController : ControllerBase
	{
		private readonly ISurveyRepository<Question> _repository;

		public QuestionController ( ISurveyRepository<Question> repository) => _repository = repository;

		[HttpGet]
		[Route ( "list" )]
		public async Task<IEnumerable<Question>> GetQuestions () => await _repository.GetCollectionAsync ( "QuestionOptions" );

	}

}
