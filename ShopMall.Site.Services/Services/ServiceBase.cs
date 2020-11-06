using Dapper;
using ShopMall.Site.Domain.IRepositories;
using ShopMall.Site.Infrastructure.DapperExtensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopMall.Site.Services.Services
{
    public class ServiceBase : IRepositoriesBase
    {
        public IEnumerable<TEntity> Entities<TEntity>(string sql)
        {
            return DB.ConnRead().Query<TEntity>(sql);
        }

        public Task<IEnumerable<TEntity>> EntitiesAsync<TEntity>(string sql)
        {
            return DB.ConnRead().QueryAsync<TEntity>(sql);
        }
    }
}
