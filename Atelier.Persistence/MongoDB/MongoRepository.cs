using Amazon.Auth.AccessControlPolicy;
using Atelier.Application.Interfaces.Repository;
using Atelier.Common.Dto;
using Atelier.Domain.MongoEntities;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static System.Collections.Specialized.BitVector32;

namespace Atelier.Persistence.MongoDB
{
    public class MongoRepository<T>: IMongoRepository<T> where T : IEntity
    {
        private readonly IMongoCollection<T> collection;
        private readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;
        public MongoRepository(IMongoDatabase database,string collectionName)
        {
            collection = database.GetCollection<T>(collectionName);
        }
        public async Task<(IReadOnlyCollection<T>,long? Total)> GetAllAsync(RequstPaginateDto? paginate, IClientSessionHandle session = null)
        {
            try
            {
                FilterDefinition<T> isRemoveFilter = filterBuilder.Eq(e => e.IsRemoved, false);
                long total = 0;
                IReadOnlyCollection<T> resultCollection;
                if(session==null)
                {
                    resultCollection = await collection.Find(isRemoveFilter).ToListAsync();
                    return (resultCollection, total);
                }
                else
                {
                    resultCollection = await collection.Find(session,isRemoveFilter).ToListAsync();
                    return (resultCollection, total);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<(IReadOnlyCollection<T>,long? Total)> GetAllAsync(Expression<Func<T,bool>> filter, RequstPaginateDto? paginate, IClientSessionHandle session = null)
        {
            try
            {
                FilterDefinition<T> isRemoveFilter = filterBuilder.Eq(e => e.IsRemoved, false);
                FilterDefinition<T> combinedFilter = filter & isRemoveFilter;
                long total = await collection.Find(combinedFilter).CountAsync();
                IReadOnlyCollection<T> resultCollection;
                if(session== null)
                {
                    if (paginate == null)
                    {
                        resultCollection = await collection.Find(combinedFilter).ToListAsync();
                    }
                    else
                    {
                        resultCollection = await collection.Find(combinedFilter)
                            .Skip((paginate.Page - 1) * paginate.PageSize)
                            .Limit(paginate.PageSize)
                            .ToListAsync();
                    }
                    return (resultCollection, total);
                }
                else
                {
                    if (paginate == null)
                    {
                        resultCollection = await collection.Find(session,combinedFilter).ToListAsync();
                    }
                    else
                    {
                        resultCollection = await collection.Find(session, combinedFilter)
                            .Skip((paginate.Page - 1) * paginate.PageSize)
                            .Limit(paginate.PageSize)
                            .ToListAsync();
                    }
                    return (resultCollection, total);
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<T> GetAsync(Guid id, IClientSessionHandle session = null)
        {
            try
            {
                FilterDefinition<T> filter = filterBuilder.Eq(e => e.Id, id);
                FilterDefinition<T> isRemoveFilter = filterBuilder.Eq(e => e.IsRemoved, false);
                FilterDefinition<T> combinedFilter = filter & isRemoveFilter;
                if(session==null)
                {
                    return await collection.Find(combinedFilter).FirstOrDefaultAsync();
                }
                else
                {
                    return await collection.Find(session, combinedFilter).FirstOrDefaultAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        public async Task<T> GetAsync(Expression<Func<T, bool>> filter, IClientSessionHandle session = null)
        {
            try
            {
                FilterDefinition<T> isRemoveFilter = filterBuilder.Eq(e => e.IsRemoved, false);
                FilterDefinition<T> combinedFilter = filter & isRemoveFilter;
                if(session==null)
                {
                    return await collection.Find(combinedFilter).FirstOrDefaultAsync();
                }
                else
                {
                    return await collection.Find(session,combinedFilter).FirstOrDefaultAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public async Task<T> GetLastAsync(Expression<Func<T, bool>> filter, IClientSessionHandle session = null)
        {
            try
            {
                FilterDefinition<T> isRemoveFilter = filterBuilder.Eq(e => e.IsRemoved, false);
                FilterDefinition<T> combinedFilter = filter & isRemoveFilter;
                if(session==null)
                {
                    return await collection.Find(combinedFilter).Limit(1).SortByDescending(q => q.InsertTime).FirstOrDefaultAsync();
                }
                else
                {
                    return await collection.Find(session,combinedFilter).Limit(1).SortByDescending(q => q.InsertTime).FirstOrDefaultAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public async Task CreateAsync(T entity, IClientSessionHandle session = null)
        {
            if(session==null)
            {
                await collection.InsertOneAsync(entity);
            }
            else
            {
                await collection.InsertOneAsync(session,entity);
            }
        }

        public async Task UpdateAsync(T entity, IClientSessionHandle session = null)
        {
            try
            {
                FilterDefinition<T> filter = filterBuilder.Eq(e => e.Id, entity.Id);
                FilterDefinition<T> isRemoveFilter = filterBuilder.Eq(e => e.IsRemoved, false);
                FilterDefinition<T> combinedFilter = filter & isRemoveFilter;

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
                // Handle the exception as needed
                throw ex;
            }
        }
        public async Task RemoveAsync(Guid id, IClientSessionHandle session = null)
        {
            try
            {
                FilterDefinition<T> filter = filterBuilder.Eq(e => e.Id, id);
                FilterDefinition<T> isRemoveFilter = filterBuilder.Eq(e => e.IsRemoved, false);
                FilterDefinition<T> combinedFilter = filter & isRemoveFilter;
                if(session==null)
                {
                    await collection.DeleteOneAsync(combinedFilter);
                }
                else
                {
                    await collection.DeleteOneAsync(session, combinedFilter);
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        public async Task<IClientSessionHandle> StartSessionAsync()
        {
            return await collection.Database.Client.StartSessionAsync();
        }
    }
}
