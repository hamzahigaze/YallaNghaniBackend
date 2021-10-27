using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using YallaNghani.Helpers.Pagination;
using YallaNghani.Models.Core;

namespace YallaNghani.Repositories.Core
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        public Task<PagedList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression,
                                        PaginationParameters paginationParameters = default);

        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);

        public Task AddAsync(TEntity entity);

        public Task AddAsync(IEnumerable<TEntity> entities);

        public Task UpdateAsync(IEnumerable<TEntity> entities);

        public Task UpdateAsync(TEntity entity);

        public Task DeleteAsync(TEntity entity);

        public Task DeleteAsync(IEnumerable<TEntity> entities);
    }
}
