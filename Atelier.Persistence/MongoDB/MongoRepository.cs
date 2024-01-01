using Atelier.Application.Interfaces.Repository;
using Atelier.Common.Dto;
using Atelier.Domain.MongoEntities;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Atelier.Persistence.MongoDB
{
    public class MongoRepository<T> : IMongoRepository<T> where T : IEntity
    {
        private readonly IMongoCollection<T> collection;
        private readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;

        public MongoRepository(IMongoDatabase database, string collectionName)
        {
            collection = database.GetCollection<T>(collectionName);
        }

        public async Task<(IReadOnlyCollection<T>, long? Total)> GetAllAsync(RequstPaginateDto? paginate, IClientSessionHandle session = null)
        {
            try
            {
                var isRemoveFilter = filterBuilder.Eq(e => e.IsRemoved, false);
                var resultCollection = session == null
                    ? await collection.Find(isRemoveFilter).ToListAsync()
                    : await collection.Find(session, isRemoveFilter).ToListAsync();

                return (resultCollection, resultCollection.Count);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<(IReadOnlyCollection<T>, long? Total)> GetAllAsync(Expression<Func<T, bool>> filter, RequstPaginateDto? paginate, IClientSessionHandle session = null)
        {
            try
            {
                var isRemoveFilter = filterBuilder.Eq(e => e.IsRemoved, false);
                var combinedFilter = filter & isRemoveFilter;

                long total = await collection.Find(combinedFilter).CountAsync();

                var resultCollection = session == null
                    ? await ApplyPagination(collection.Find(combinedFilter), paginate).ToListAsync()
                    : await ApplyPagination(collection.Find(session, combinedFilter), paginate).ToListAsync();

                return (resultCollection, total);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> GetAsync(Guid id, IClientSessionHandle session = null)
        {
            try
            {
                var filter = filterBuilder.Eq(e => e.Id, id);
                var isRemoveFilter = filterBuilder.Eq(e => e.IsRemoved, false);
                var combinedFilter = filter & isRemoveFilter;

                return session == null
                    ? await collection.Find(combinedFilter).FirstOrDefaultAsync()
                    : await collection.Find(session, combinedFilter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter, IClientSessionHandle session = null)
        {
            try
            {
                var isRemoveFilter = filterBuilder.Eq(e => e.IsRemoved, false);
                var combinedFilter = filter & isRemoveFilter;

                return session == null
                    ? await collection.Find(combinedFilter).FirstOrDefaultAsync()
                    : await collection.Find(session, combinedFilter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> GetLastAsync(Expression<Func<T, bool>> filter, IClientSessionHandle session = null)
        {
            try
            {
                var isRemoveFilter = filterBuilder.Eq(e => e.IsRemoved, false);
                var combinedFilter = filter & isRemoveFilter;

                return session == null
                    ? await collection.Find(combinedFilter).Limit(1).SortByDescending(q => q.InsertTime).FirstOrDefaultAsync()
                    : await collection.Find(session, combinedFilter).Limit(1).SortByDescending(q => q.InsertTime).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task CreateAsync(T entity, IClientSessionHandle session = null)
        {
            try
            {
                if (session == null)
                {
                    await collection.InsertOneAsync(entity);
                }
                else
                {
                    await collection.InsertOneAsync(session, entity);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateAsync(T entity, IClientSessionHandle session = null)
        {
            try
            {
                var filter = filterBuilder.Eq(e => e.Id, entity.Id);
                var isRemoveFilter = filterBuilder.Eq(e => e.IsRemoved, false);
                var combinedFilter = filter & isRemoveFilter;

                if (session == null)
                {
                    await collection.ReplaceOneAsync(combinedFilter, entity);
                }
                else
                {
                    await collection.ReplaceOneAsync(session, combinedFilter, entity);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task RemoveAsync(Guid id, IClientSessionHandle session = null)
        {
            try
            {
                var filter = filterBuilder.Eq(e => e.Id, id);
                var isRemoveFilter = filterBuilder.Eq(e => e.IsRemoved, false);
                var combinedFilter = filter & isRemoveFilter;

                if (session == null)
                {
                    await collection.DeleteOneAsync(combinedFilter);
                }
                else
                {
                    await collection.DeleteOneAsync(session, combinedFilter);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IClientSessionHandle> StartSessionAsync()
        {
            return await collection.Database.Client.StartSessionAsync();
        }

        private IFindFluent<T, T> ApplyPagination(IFindFluent<T, T> query, RequstPaginateDto? paginate)
        {
            if (paginate == null) return query;

            return query
                .Skip((paginate.Page - 1) * paginate.PageSize)
                .Limit(paginate.PageSize);
        }
    }
}
