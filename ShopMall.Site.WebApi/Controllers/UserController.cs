using Microsoft.AspNetCore.Mvc;
using ShopMall.Site.Domain.Dtos;
using ShopMall.Site.Domain.Entities;
using ShopMall.Site.Domain.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopMall.Site.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        public IUserRepositories _UserRepositories { get; set; }

 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var result = await _UserRepositories.EntitiesAsync($"SELECT * FROM [User]",null);
            return Ok(result);
        }
    }
}
