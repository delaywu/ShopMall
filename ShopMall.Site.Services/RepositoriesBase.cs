using Dapper;
using ShopMall.Site.Domain.Entities;
using ShopMall.Site.Domain.IRepositories;
using ShopMall.Site.Infrastructure.DapperExtensions;
using ShopMall.Site.Infrastructure.Entity;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ShopMall.Site.Services
{
    public class RepositoriesBase<TEntity, TKey> : IRepositoriesBase<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        public IEnumerable<TEntity> Entities(string sql, object param, CommandType commandType = CommandType.Text)
        {
            return DB.Read().Query<TEntity>(sql,param, commandType: commandType);
        }

        public Task<IEnumerable<TEntity>> EntitiesAsync(string sql, object param, CommandType commandType = CommandType.Text)
        {
            return DB.Read().QueryAsync<TEntity>(sql, param, commandType: commandType);
        }
    }
}
