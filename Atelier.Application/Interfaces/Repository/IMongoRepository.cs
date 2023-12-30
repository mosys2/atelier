using Atelier.Common.Dto;
using Atelier.Domain.MongoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Interfaces.Repository
{
    public interface IMongoRepository<T> where T : IEntity
    {
        Task CreateAsync(T entity);
        Task<(IReadOnlyCollection<T>,long? Total)> GetAllAsync(RequstPaginateDto? paginate);
        Task<(IReadOnlyCollection<T>,long? Total)> GetAllAsync(Expression<Func<T, bool>> filter, RequstPaginateDto? paginate);
        Task<T> GetAsync(Guid id);
        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        Task<T> GetLastAsync(Expression<Func<T, bool>> filter);
        Task RemoveAsync(Guid id);
        Task UpdateAsync(T entity);
    }
}
