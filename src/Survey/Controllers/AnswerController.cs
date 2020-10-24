using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Survey.Model.Entities;
using Survey.Model.Repositories;
using Survey.Models;
using SurveyValueType = Survey.Model.Entities.ValueType;

namespace Survey.Controllers
{
	[Route ( "api/answers" )]
	[ApiController]
	public class AnswerController : ControllerBase
	{
		private readonly ISurveyRepository<Answer> _repository;

		public AnswerController ( ISurveyRepository<Answer> repository ) => _repository = repository;

		private object GetJsonValue ( JsonElement jsonElement ) {
			return jsonElement.GetString ();
		}

		[HttpPost]
		[Route ( "save" )]
		public bool SaveAnswers ( [FromBody] IEnumerable<AnswerModel> answers ) {
			var questionary = new Questionary {
				Created = DateTime.UtcNow
			};

			_repository.AddRange (
				answers.Select (
					a => new Answer {
						ValueType = a.ValueType ,
						Questionary = questionary ,
						BoolValue = a.ValueType == SurveyValueType.Bool ? ( (JsonElement) a.Value ).GetBoolean () : (bool?) null ,
						DateValue = a.ValueType == SurveyValueType.Date ? ( (JsonElement) a.Value ).GetDateTime () : (DateTime?) null ,
						IntegerValue = a.ValueType == SurveyValueType.Integer || a.ValueType == SurveyValueType.Enum ? ( (JsonElement) a.Value ).GetInt32() : (int?) null ,
						StringValue = a.ValueType == SurveyValueType.String ? ( (JsonElement) a.Value ).GetString() : (string) null ,
						AnswerQuestions = new List<AnswerQuestion> {
							new AnswerQuestion {
								QuestionId = a.QuestionId
							}
						}
					}
				).ToList ()
			);

			try {
				_repository.SaveAsync ();
			} catch {
				return false;
			}

			return true;
		}

	}

}
