using Survey.Common;
using Survey.Model.Entities;
using Survey.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.BL.Services
{
	public class QuestionService : IQuestionService
	{

		private readonly ISurveyRepository<Question> _repository;

		public QuestionService ( ISurveyRepository<Question> repository ) => _repository = repository ?? throw new ArgumentNullException(nameof( repository ) );

		public async Task<IEnumerable<QuestionModel>> GetQuestions () {
			var questions = await _repository.GetCollectionAsync ( "QuestionOptions" );

			return questions
				.Select (
					question => new QuestionModel {
						Id = question.Id,
						Title = question.Title,
						ValueType = question.ValueType,
						QuestionOptions = question.QuestionOptions?
							.Select(
								questionOption => new QuestionOptionModel {
									Id = questionOption.Id,
									Title = questionOption.Title
								}
							)
							.ToList()
					}
				)
				.ToList();
		}


	}
}
