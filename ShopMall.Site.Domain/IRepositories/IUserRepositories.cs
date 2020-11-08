using System.Collections.Generic;
using System.Threading.Tasks;
using ShopMall.Site.Domain.Dtos;

namespace ShopMall.Site.Domain.IRepositories
{
    public interface IUserRepositories : IDependency
    {
        IEnumerable<UserDto> Entities(string sql, object param);
        Task<IEnumerable<UserDto>> EntitiesAsync(string sql, object param);
    }
}
