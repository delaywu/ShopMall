using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopMall.Site.Domain.IRepositories;
using ShopMall.Site.Domain.Models;

namespace ShopMall.Site.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController:ControllerBase
    {
        public IHomeRepositories _HomeRepositories { get; set; }
        
        [HttpGet(Name =nameof(GetHomeData))] 
        public async Task<ActionResult<IEnumerable<HomeModel>>> GetHomeData()
        {
            var result = await _HomeRepositories.HomeEntitiesAsync(1);
            return Ok(result);
        }

        //public async Task<AcceptedResult<IEnumerable<>>>
    }
}
