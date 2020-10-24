using System.Collections.Generic;
using System.Threading.Tasks;

namespace Survey.Model.Repositories
{

	/// <summary>
	/// Репозиторий для хранилища survey.
	/// </summary>
	/// <typeparam name="T">Тип entity.</typeparam>
	public interface ISurveyRepository<T> where T : class
	{
		/// <summary>
		/// Получить коллекцию объектов.
		/// </summary>
		Task<IEnumerable<T>> GetCollectionAsync (string includes = "");

		/// <summary>
		/// Получение одного объекта.
		/// </summary>
		/// <param name="id">Идентификатор объекта.</param>
		Task<T> GetAsync ( int id );

		/// <summary>
		/// Добавить объект.
		/// </summary>
		/// <param name="item">Объект для добавления.</param>
		void Add ( T item );

		/// <summary>
		/// Добавить коллекцию объектов.
		/// </summary>
		/// <param name="item">Коллекция объектов для добавления.</param>
		void AddRange ( IEnumerable<T> item );

		/// <summary>
		/// Сохранение изменений в хранилище.
		/// </summary>
		Task SaveAsync ();

	}

}
