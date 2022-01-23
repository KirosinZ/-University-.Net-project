using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Business.Repositories;
using Microsoft.EntityFrameworkCore;
using Repository.Data;

namespace Repository
{
    public abstract class AbstractRepository<T, TKey> : IRepository<T, TKey>
		where T : class
    {
		private readonly object _syncRoot = new();
		protected Context _context;

		#region protected

		protected void CreateOrUpdateImplementation(T value)
		{
			var entity = ReadImplementation(KeySelector(value));
			if (entity == null) CreateImplementation(value);
			else UpdateImplementation(value);
		}

		protected T ReadImplementation(TKey key)
        {
			return QueryImplementation().FirstOrDefault(entity => KeySelector(entity).Equals(key));
        }

		protected void CreateImplementation(T value)
        {
			_context.Add(value);
        }
		
		protected void UpdateImplementation(T value)
        {
			_context.ChangeTracker.Clear();
			_context.Update(value);
        }

		protected void DeleteImplementation(T value)
        {
			_context.ChangeTracker.Clear();
			_context.Remove(value);
        }

		protected void OperationEnvironment(Action body)
		{
			lock (_syncRoot)
			{

				body.Invoke();
				try
				{
					_context.SaveChanges();
				}
				catch (DbUpdateConcurrencyException ex)
				{
					ex.Entries.Single().Reload();
					_context.SaveChanges();
				}

			}
		}

		protected TRet OperationEnvironment<TRet>(Func<TRet> body)
		{
			lock (_syncRoot)
			{
				return body.Invoke();
			}
		}

		#region abstract
		protected abstract TKey KeySelector(T value);

		protected abstract IQueryable<T> QueryImplementation();
		#endregion
		#endregion

		#region interface
		public T Read(TKey key)
		{
			return OperationEnvironment(() => ReadImplementation(key));
		}

		public void Create(T value)
		{
			OperationEnvironment(() => CreateImplementation(value));
		}

		public void Update(T value)
		{
			OperationEnvironment(() => UpdateImplementation(value));
		}

		public void CreateOrUpdate(T value)
		{
			OperationEnvironment(() => CreateOrUpdateImplementation(value));
		}

		public void Delete(T value)
		{
			OperationEnvironment(() => DeleteImplementation(value));
		}

		public void Create(IEnumerable<T> values)
		{
			OperationEnvironment(() => values.ToList().ForEach(CreateImplementation));
		}

		public void Update(IEnumerable<T> values)
		{
			OperationEnvironment(() => values.ToList().ForEach(v => UpdateImplementation(v)));
		}

		public void CreateOrUpdate(IEnumerable<T> values)
		{
			OperationEnvironment(() => values.ToList().ForEach(CreateOrUpdateImplementation));
		}

		public void Delete(IEnumerable<T> values)
		{
			OperationEnvironment(() => values.ToList().ForEach(DeleteImplementation));
		}

		public List<T> Query(Expression<Func<T, bool>> where = null)
		{
			return OperationEnvironment(() =>
			{
				var query = QueryImplementation();
				if (where != null) query = query.Where(where);
				return query.ToList();
			});
		}

		public TResult Query<TResult>(Func<IQueryable<T>, TResult> body)
		{
			return OperationEnvironment(() =>
			{
				var query = QueryImplementation();
				return body(query);
			});
		}
		#endregion
	}
}
