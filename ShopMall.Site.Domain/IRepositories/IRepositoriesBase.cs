using ShopMall.Site.Domain.Entities;
using ShopMall.Site.Infrastructure.Entity;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ShopMall.Site.Domain.IRepositories
{
    public interface IRepositoriesBase<TEntity,TKey> : IDependency where TEntity : IEntity<TKey>
    {
        IEnumerable<TEntity> Entities(string sql, object param, CommandType commandType = CommandType.Text);

        Task<IEnumerable<TEntity>> EntitiesAsync(string sql, object param, CommandType commandType = CommandType.Text);



    }
}
