using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopMall.Site.Domain.IRepositories
{
    public interface IRepositoriesBase : IDependency
    {
        IEnumerable<TEntity> Entities<TEntity>(string sql);

        Task<IEnumerable<TEntity>> EntitiesAsync<TEntity>(string sql);



    }
}
