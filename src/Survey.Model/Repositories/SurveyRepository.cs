using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.Model.Repositories
{

	public class SurveyRepository<T> : ISurveyRepository<T> where T : class
	{
		private readonly SurveyContext _surveyContext;

		public SurveyRepository (SurveyContext surveyContext) => _surveyContext = surveyContext ?? throw new ArgumentNullException ( nameof ( surveyContext ) );

		public void Add ( T item ) => _surveyContext.Set<T> ().Add ( item );

		public void AddRange ( IEnumerable<T> item ) => _surveyContext.Set<T> ().AddRange ( item );

		public async Task<T> GetAsync ( int id ) => await _surveyContext.Set<T> ().FindAsync ( id );

		public async Task<IEnumerable<T>> GetCollectionAsync ( string includes = "" ) {
			var collection = (IQueryable<T>)_surveyContext.Set<T> ();

			if (!string.IsNullOrEmpty(includes)) collection = collection.Include( "QuestionOptions" );
			
			return await collection.ToListAsync ();
		}

		public Task SaveAsync () => _surveyContext.SaveChangesAsync ();

	}

}
