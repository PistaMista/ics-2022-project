using AutoMapper;
using CarPool.DAL.Entities;

namespace CarPool.BL.Models;
public record PassengerInfoModel() : ModelBase
{
    public class MapperProfile : Profile
    {
        //public MapperProfile()
        //{
        //    CreateMap<UserEntity, UserInfoModel>();
        //}
    }
}
