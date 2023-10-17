using LiteDB;

namespace LiteDBApp.DB
{
	public interface ILiteDbRepository<TEntity> where TEntity : BaseEntity
	{
		TEntity GetById(int id);
		IEnumerable<TEntity> GetAll();
		void Insert(TEntity entity);
		void Update(TEntity entity);
		void Delete(int id);
	}
	public class LiteDbRepository<TEntity> : ILiteDbRepository<TEntity> where TEntity : BaseEntity
	{
		private readonly ILiteCollection<TEntity> _collection;

		public LiteDbRepository(ILiteDbContext dbContext)
		{
			_collection = dbContext.Database.GetCollection<TEntity>(typeof(TEntity).Name);
		}

		public TEntity GetById(int id)
		{
			return _collection.FindById(id);
		}

		public IEnumerable<TEntity> GetAll()
		{
			return _collection.FindAll();
		}

		public void Insert(TEntity entity)
		{
			_collection.Insert(entity);
		}

		public void Update(TEntity entity)
		{
			_collection.Update(entity);
		}

		public void Delete(int id)
		{
			_collection.Delete(id);
		}
	}

}
