using Microsoft.AspNetCore.Mvc;
using ShopMall.Site.Domain.Entities;
using ShopMall.Site.Domain.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopMall.Site.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        public IUserRepositories _UserRepositories { get; set; }

 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var result = await _UserRepositories.EntitiesAsync<User>($"SELECT * FROM SM_User");
            return Ok(result);
        }
    }
}
