using ShopMall.Site.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopMall.Site.Domain.IRepositories
{
    public interface IRepositoriesBase<TEntity,TKey> : IDependency where TEntity : EntityBase<TKey>
    {
        IEnumerable<TEntity> Entities(string sql, object param);

        Task<IEnumerable<TEntity>> EntitiesAsync(string sql, object param);



    }
}
