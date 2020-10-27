using FakeItEasy;
using Survey.BL.Services;
using Survey.Model.Entities;
using Survey.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Survey.UnitTests
{
	public class QuestionServiceUnitTests
	{

		[Fact]
		public void Constructor_Repository_Null_Throw () {
			Assert.Throws<ArgumentNullException> (
				() => {
					var service = new QuestionService ( null );
				}
			);
		}

		[Fact]
		public void Constructor_Success () {
			var fakeDependency = A.Fake<ISurveyRepository<Question>> ();

			var service = new QuestionService ( fakeDependency );

			Assert.True ( true );
		}

		[Fact]
		public async Task GetQuestions_Check_Mapping_Question () {
			var fakeDependency = A.Fake<ISurveyRepository<Question>> ();

			var collection = new List<Question> {
				new Question {
					Id = 1,
					Title = "test",
					ValueType = Model.Entities.ValueType.Date,
					QuestionOptions = new List<QuestionOption>()
				}
			};

			A.CallTo ( () => fakeDependency.GetCollectionAsync ( "QuestionOptions" ) ).Returns ( collection );

			var service = new QuestionService ( fakeDependency );

			var questions = await service.GetQuestions ();

			Assert.True ( questions.Count() == 1 );
			var question = questions.First ();
			Assert.Equal ( 1, question.Id );
			Assert.Equal ( "test", question.Title );
			Assert.Equal ( Model.Entities.ValueType.Date, question.ValueType );
		}

		[Fact]
		public async Task GetQuestions_Check_Mapping_QuestionOptions () {
			var fakeDependency = A.Fake<ISurveyRepository<Question>> ();

			var collection = new List<Question> {
				new Question {
					Id = 1,
					Title = "test",
					ValueType = Model.Entities.ValueType.Date,
					QuestionOptions = new List<QuestionOption>{
						new QuestionOption {
							Id = 2,
							Title = "option"
						}
					}
				}
			};

			A.CallTo ( () => fakeDependency.GetCollectionAsync ( "QuestionOptions" ) ).Returns ( collection );

			var service = new QuestionService ( fakeDependency );

			var questions = await service.GetQuestions ();

			var questionOptions = questions.First ().QuestionOptions;
			Assert.True ( questionOptions.Count () == 1 );
			var questionOption = questionOptions.First ();
			Assert.Equal ( 2 , questionOption.Id );
			Assert.Equal ( "option" , questionOption.Title );
		}

	}
}
