using FakeItEasy;
using Survey.BL;
using Survey.Model.Entities;
using Survey.Model.Repositories;
using Survey.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Survey.UnitTests
{
	public class AnswerServiceUnitTests
	{
		[Fact]
		public void Constructor_Repository_Null_Throw () {
			Assert.Throws<ArgumentNullException> (
				() => {
					var service = new AnswerService ( null );
				}
			);
		}

		[Fact]
		public void Constructor_Success () {
			var fakeDependency = A.Fake<ISurveyRepository<Answer>> ();

			var service = new AnswerService ( fakeDependency );

			Assert.True ( true );
		}

		[Fact]
		public void SaveAnswers_Empty_Collection () {
			var fakeDependency = A.Fake<ISurveyRepository<Answer>> ();

			var task = Task.FromResult ( false );

			A.CallTo ( () => fakeDependency.SaveAsync () ).Returns ( task );

			var service = new AnswerService ( fakeDependency );

			Assert.Equal ( task , service.SaveAnswersAsync ( Enumerable.Empty<AnswerModel> () ) );
		}

		[Fact]
		public void SaveAnswers_Check_Mapping_Bool () {
			var fakeDependency = A.Fake<ISurveyRepository<Answer>> ();

			A.CallTo ( () => fakeDependency.SaveAsync () ).DoesNothing();

			var service = new AnswerService ( fakeDependency );

			service.SaveAnswersAsync (
				new List<AnswerModel> {
					new AnswerModel {
						QuestionId = 0,
						Value = JsonDocument.Parse ( "true" ).RootElement,
						ValueType = Model.Entities.ValueType.Bool
					}
				}
			);

			Assert.True ( true );
		}

		[Fact]
		public void SaveAnswers_Check_Mapping_String () {
			var fakeDependency = A.Fake<ISurveyRepository<Answer>> ();

			A.CallTo ( () => fakeDependency.SaveAsync () ).DoesNothing ();

			var service = new AnswerService ( fakeDependency );

			service.SaveAnswersAsync (
				new List<AnswerModel> {
					new AnswerModel {
						QuestionId = 0,
						Value = JsonDocument.Parse ( "\"test\"" ).RootElement,
						ValueType = Model.Entities.ValueType.String
					}
				}
			);

			Assert.True ( true );
		}

		[Fact]
		public void SaveAnswers_Check_Mapping_Number () {
			var fakeDependency = A.Fake<ISurveyRepository<Answer>> ();

			A.CallTo ( () => fakeDependency.SaveAsync () ).DoesNothing ();

			var service = new AnswerService ( fakeDependency );

			service.SaveAnswersAsync (
				new List<AnswerModel> {
					new AnswerModel {
						QuestionId = 0,
						Value = JsonDocument.Parse ( "1" ).RootElement,
						ValueType = Model.Entities.ValueType.Integer
					}
				}
			);

			Assert.True ( true );
		}

		[Fact]
		public void SaveAnswers_Check_Mapping_Enum () {
			var fakeDependency = A.Fake<ISurveyRepository<Answer>> ();

			A.CallTo ( () => fakeDependency.SaveAsync () ).DoesNothing ();

			var service = new AnswerService ( fakeDependency );

			service.SaveAnswersAsync (
				new List<AnswerModel> {
					new AnswerModel {
						QuestionId = 0,
						Value = JsonDocument.Parse ( "1" ).RootElement,
						ValueType = Model.Entities.ValueType.Enum
					}
				}
			);

			Assert.True ( true );
		}

		[Fact]
		public void SaveAnswers_Check_Mapping_Date () {
			var fakeDependency = A.Fake<ISurveyRepository<Answer>> ();

			A.CallTo ( () => fakeDependency.SaveAsync () ).DoesNothing ();

			var service = new AnswerService ( fakeDependency );

			service.SaveAnswersAsync (
				new List<AnswerModel> {
					new AnswerModel {
						QuestionId = 0,
						Value = JsonDocument.Parse ( "\"2019-07-26T00:00:00\"" ).RootElement,
						ValueType = Model.Entities.ValueType.Date
					}
				}
			);

			Assert.True ( true );
		}

	}

}
