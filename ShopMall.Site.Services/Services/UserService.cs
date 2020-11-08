using AutoMapper;
using ShopMall.Site.Domain.Dtos;
using ShopMall.Site.Domain.Entities;
using ShopMall.Site.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopMall.Site.Services.Services
{
    public class UserService : ServiceBase, IUserRepositories
    {
        public IRepositoriesBase<User, int> UserRepositories { protected get; set; }

        private readonly IMapper _mapper;

        public UserService(IMapper mapper)
        {
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); 
        }


        public IEnumerable<UserDto> Entities(string sql, object param)
        {
            var users= UserRepositories.Entities(sql, param);
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<IEnumerable<UserDto>> EntitiesAsync(string sql, object param)
        {
            var users = await UserRepositories.EntitiesAsync(sql, param);
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }
    }
}
