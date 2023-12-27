using Atelier.Application.Interfaces.Repository;
using Atelier.Domain.MongoEntities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Persistence.MongoDB
{
    public class MongoRepository<T>: IMongoRepository<T> where T : IEntity
    {
        private readonly IMongoCollection<T> collection;
        private readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;
        public MongoRepository(IMongoDatabase database,string collectionName)
        {
            collection=database.GetCollection<T>(collectionName);
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            FilterDefinition<T> isRemoveFilter = filterBuilder.Eq(e => e.IsRemoved, false);
            return await collection.Find(isRemoveFilter).ToListAsync();
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T,bool>> filter)
        {
            FilterDefinition<T> isRemoveFilter = filterBuilder.Eq(e => e.IsRemoved, false);
            FilterDefinition<T> combinedFilter = filter & isRemoveFilter;

            return await collection.Find(combinedFilter).ToListAsync();
        }

        public async Task<T> GetAsync(Guid id)
        {
            FilterDefinition<T> filter=filterBuilder.Eq(e=>e.Id,id);
            FilterDefinition<T> isRemoveFilter = filterBuilder.Eq(e => e.IsRemoved,false);
            FilterDefinition<T> combinedFilter = filter & isRemoveFilter;
            return await collection.Find(combinedFilter).FirstOrDefaultAsync();
        }
        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            FilterDefinition<T> isRemoveFilter = filterBuilder.Eq(e => e.IsRemoved, false);
            FilterDefinition<T> combinedFilter = filter & isRemoveFilter;
            return await collection.Find(combinedFilter).FirstOrDefaultAsync();
        }
        public async Task CreateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await collection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            FilterDefinition<T> filter = filterBuilder.Eq(e => e.Id, entity.Id);
            FilterDefinition<T> isRemoveFilter = filterBuilder.Eq(e => e.IsRemoved, false);
            FilterDefinition<T> combinedFilter = filter & isRemoveFilter;

            await collection.ReplaceOneAsync(combinedFilter, entity);
        }

        public async Task RemoveAsync(Guid id)
        {
            FilterDefinition<T> filter = filterBuilder.Eq(e => e.Id, id);
            FilterDefinition<T> isRemoveFilter = filterBuilder.Eq(e => e.IsRemoved, false);
            FilterDefinition<T> combinedFilter = filter & isRemoveFilter;

            await collection.DeleteOneAsync(combinedFilter);
        }

    }
}
