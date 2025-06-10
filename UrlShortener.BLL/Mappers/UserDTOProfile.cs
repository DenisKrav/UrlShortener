using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.BLL.DTOs.User.Request;
using UrlShortener.BLL.DTOs.User.Response;
using UrlShortener.DAL.Models.Identities;

namespace UrlShortener.BLL.Mappers
{
    public class UserDTOProfile: Profile
    {
        public UserDTOProfile()
        {
            CreateMap<NewUserDTO, ApplicationUser>();
            CreateMap<UpdateUserDTO, ApplicationUser>();
            CreateMap<ApplicationUser, UserDTO>();
        }
    }
}
