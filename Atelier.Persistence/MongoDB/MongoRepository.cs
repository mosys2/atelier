using Amazon.Auth.AccessControlPolicy;
using Atelier.Application.Interfaces.Repository;
using Atelier.Common.Dto;
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

        public async Task<(IReadOnlyCollection<T>,long? Total)> GetAllAsync(RequstPaginateDto? paginate)
        {
            FilterDefinition<T> isRemoveFilter = filterBuilder.Eq(e => e.IsRemoved, false);
            long total = 0;
            IReadOnlyCollection<T> resultCollection;
            resultCollection= await collection.Find(isRemoveFilter).ToListAsync();
            return (resultCollection, total);
        }

        [Obsolete]
        public async Task<(IReadOnlyCollection<T>,long? Total)> GetAllAsync(Expression<Func<T,bool>> filter, RequstPaginateDto? paginate)
        {
            FilterDefinition<T> isRemoveFilter = filterBuilder.Eq(e => e.IsRemoved, false);
            FilterDefinition<T> combinedFilter = filter & isRemoveFilter;
            long total =await collection.Find(combinedFilter).CountAsync();
            IReadOnlyCollection<T> resultCollection;

            if (paginate==null)
            {
                resultCollection= await collection.Find(combinedFilter).SortByDescending(q => q.InsertTime).ToListAsync();
            }

            resultCollection= await collection.Find(combinedFilter)
                .SortByDescending(q => q.InsertTime)
                .Skip((paginate?.Page-1)*paginate?.PageSize)
                .Limit(paginate?.PageSize)
                .ToListAsync();
            return(resultCollection,total);
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

        public async Task<T> GetLastAsync(Expression<Func<T, bool>> filter)
        {
            FilterDefinition<T> isRemoveFilter = filterBuilder.Eq(e => e.IsRemoved, false);
            FilterDefinition<T> combinedFilter = filter & isRemoveFilter;
            return await collection.Find(combinedFilter).Limit(1).SortByDescending(q=>q.InsertTime).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(T entity)
        {
            await collection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
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
