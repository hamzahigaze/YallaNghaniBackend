using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using YallaNghani.Helpers.Pagination;
using YallaNghani.Models.Core;

namespace YallaNghani.Repositories.Core
{
    public abstract class MongoRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        #region Feilds
        protected readonly IMongoCollection<TEntity> _collection;
        #endregion

        #region Constructor
        public MongoRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<TEntity>(typeof(TEntity).Name);
        }
        #endregion

        #region Add
        public async Task AddAsync(TEntity entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task AddAsync(IEnumerable<TEntity> entities)
        {
            await _collection.InsertManyAsync(entities);
        }
        #endregion

        #region Delete
        public async Task DeleteAsync(TEntity entity)
        {
            await _collection.DeleteOneAsync<TEntity>(e => e.Id == entity.Id);
        }

        public async Task DeleteAsync(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                await DeleteAsync(entity);
        }
        #endregion

        #region Get
        public async Task<PagedList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression,
                                                    PaginationParameters paginationParameters = null)
        {

            if (paginationParameters == null)
                paginationParameters = new PaginationParameters();

            var pageIndex = paginationParameters.PageIndex;
            var pageSize = paginationParameters.PageSize;

            var result = _collection.Find(expression);
            var count = await result.CountAsync();
            var list = await result.Skip(pageIndex * pageSize).Limit(pageSize).ToListAsync();

            return list.ToPagedList<TEntity>((int)count, pageIndex, pageSize);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _collection.Find(expression).FirstOrDefaultAsync();
        }

        #endregion

        #region Update
        public async Task UpdateAsync(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                await UpdateAsync(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            var result = await GetAsync(e => e.Id == entity.Id);
            
            if (result != null)
            {
                //entity.FillMissingProperties(result);
                await _collection.ReplaceOneAsync(e => e.Id == entity.Id, entity,
                                                new ReplaceOptions { IsUpsert = false });
            }

        }
        #endregion
    }
}
