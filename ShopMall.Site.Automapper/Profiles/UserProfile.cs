using AutoMapper; 
using ShopMall.Site.Domain.Dtos;
using ShopMall.Site.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopMall.Site.Automapper.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
