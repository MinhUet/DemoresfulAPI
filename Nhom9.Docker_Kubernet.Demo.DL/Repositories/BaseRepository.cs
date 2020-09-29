using MongoDB.Bson;
using MongoDB.Driver;
using Nhom9.Docker_Kubernet.Demo.Common.Utilities;
using Nhom9.Docker_Kubernet.Demo.DL.Database;
using Nhom9.Docker_Kubernet.Demo.DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nhom9.Docker_Kubernet.Demo.DL.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly string _collectionName;
        private readonly MongoDatabaseContext _databaseContext;
        protected readonly IMongoCollection<T> _collection;
        public BaseRepository(IDatabaseContext databaseContext)
        {
            _collectionName = Utility.GetEntityName<T>();
            _databaseContext = (MongoDatabaseContext)databaseContext;
            _collection = _databaseContext.MongoDatabase.GetCollection<T>(_collectionName);
        }

        public virtual async Task<long> Delete(string id)
        {
            var parameterExpression = Expression.Parameter(typeof(T), "object");
            var propertyOrFieldExpression = Expression.PropertyOrField(parameterExpression, typeof(T).Name + "ID");
            var equalityExpression = Expression.Equal(propertyOrFieldExpression, Expression.Constant(id, typeof(string)));
            var lambdaExpression = Expression.Lambda<Func<T, bool>>(equalityExpression, parameterExpression);
            var deleteResult = await _collection.DeleteOneAsync(lambdaExpression);
            return deleteResult.DeletedCount;
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            var entities = await _collection.Find(new BsonDocument()).ToListAsync();
            return entities;
        }
        
        public virtual async Task<IEnumerable<T>> GetSkipTake(int page, int take = 10)
        {
            var skip = page > 0 ? page - 1 : 0;
            var entities = await _collection.Find(new BsonDocument())
                .Skip(skip)
                .Limit(take)
                .ToListAsync();
            return entities;
        }

        public virtual async Task<T> GetById(string id)
        {
            var parameterExpression = Expression.Parameter(typeof(T), "object");
            var propertyOrFieldExpression = Expression.PropertyOrField(parameterExpression, typeof(T).Name + "ID");
            var equalityExpression = Expression.Equal(propertyOrFieldExpression, Expression.Constant(id, typeof(string)));
            var lambdaExpression = Expression.Lambda<Func<T, bool>>(equalityExpression, parameterExpression);
            var entity = await (await _collection.FindAsync(lambdaExpression)).FirstAsync();
            return entity;
        }

        public virtual Task Update(T entity)
        {
            throw new NotImplementedException();
        }

        public async virtual Task<string> Insert(T entity)
        {
            try
            {
                await _collection.InsertOneAsync(entity);
                return (string)entity.GetType().GetProperty(typeof(T).Name + "ID").GetValue(entity);
            }
            catch (Exception ex)
            {
                return "Fail: " + ex;
            }
        }

        public async virtual Task<string> InsertMany(List<T> entity)
        {
            try
            {
                await _collection.InsertManyAsync(entity);
                return "Insert Success";
            }
            catch (Exception ex)
            {
                return "Fail: " + ex;
            }
        }
    }
}
