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

namespace Atelier.Application.Interfaces.Repository
{
    public interface IMongoRepository<T> where T : IEntity
    {
        Task CreateAsync(T entity, IClientSessionHandle session = null);
        Task<(IReadOnlyCollection<T>,long? Total)> GetAllAsync(RequstPaginateDto? paginate, IClientSessionHandle session = null);
        Task<(IReadOnlyCollection<T>,long? Total)> GetAllAsync(Expression<Func<T, bool>> filter, RequstPaginateDto? paginate,IClientSessionHandle session = null);
        Task<T> GetAsync(Guid id, IClientSessionHandle session = null);
        Task<T> GetAsync(Expression<Func<T, bool>> filter, IClientSessionHandle session = null);
        Task<T> GetLastAsync(Expression<Func<T, bool>> filter, IClientSessionHandle session = null);
        Task RemoveAsync(Guid id, IClientSessionHandle session = null);
        Task UpdateAsync(T entity, IClientSessionHandle session = null);
        Task<IClientSessionHandle> StartSessionAsync();
    }
}
