using System;
using System.Collections.Generic;
using System.Text;
using ShopMall.Site.Domain.Dtos;

namespace ShopMall.Site.Domain.Models
{
    public class HomeModel
    {
        public IEnumerable<MerchantProductDto> Products { get; set; }
    }
}
