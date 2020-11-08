using Dapper;
using ShopMall.Site.Domain.Entities;
using ShopMall.Site.Domain.IRepositories;
using ShopMall.Site.Infrastructure.DapperExtensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopMall.Site.Services
{
    public class RepositoriesBase<TEntity, TKey> : IRepositoriesBase<TEntity, TKey> where TEntity : EntityBase<TKey>
    {
        public IEnumerable<TEntity> Entities(string sql, object param)
        {
            return DB.Read().Query<TEntity>(sql);
        }

        public Task<IEnumerable<TEntity>> EntitiesAsync(string sql, object param)
        {
            return DB.Read().QueryAsync<TEntity>(sql, param);
        }
    }
}
