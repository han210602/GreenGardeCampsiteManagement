using AutoMapper;
using BusinessObject.DTOs;
using BusinessObject.Models;

namespace GreenGardenCampsiteManagementAPI.ConfigAutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserDTO>();
        }
    }
}
