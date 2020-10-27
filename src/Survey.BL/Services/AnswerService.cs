using Survey.Model.Entities;
using Survey.Model.Repositories;
using Survey.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ValueType = Survey.Model.Entities.ValueType;

namespace Survey.BL
{
	public class AnswerService : IAnswerService
	{

		private readonly ISurveyRepository<Answer> _repository;

		public AnswerService ( ISurveyRepository<Answer> repository ) => _repository = repository ?? throw new ArgumentNullException ( nameof ( repository ) );

		public Task SaveAnswersAsync ( IEnumerable<AnswerModel> answers ) {
			var questionary = new Questionary {
				Created = DateTime.UtcNow
			};

			_repository.AddRange (
				answers.Select (
					a => new Answer {
						ValueType = a.ValueType ,
						Questionary = questionary ,
						BoolValue = a.ValueType == ValueType.Bool ? ( (JsonElement) a.Value ).GetBoolean () : (bool?) null ,
						DateValue = a.ValueType == ValueType.Date ? ( (JsonElement) a.Value ).GetDateTime () : (DateTime?) null ,
						IntegerValue = a.ValueType == ValueType.Integer || a.ValueType == ValueType.Enum ? ( (JsonElement) a.Value ).GetInt32 () : (int?) null ,
						StringValue = a.ValueType == ValueType.String ? ( (JsonElement) a.Value ).GetString () : null ,
						AnswerQuestions = new List<AnswerQuestion> {
							new AnswerQuestion {
								QuestionId = a.QuestionId
							}
						}
					}
				).ToList ()
			);

			return _repository.SaveAsync ();
		}

	}

}
