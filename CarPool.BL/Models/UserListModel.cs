using AutoMapper;
using DAL.Entities;

namespace CarPool.BL.Models;
public record UserListModel(
        string FirstName,
        string LastName) : ModelBase
{
    public string FirstName { get; set; } = FirstName;
    public string LastName { get; set; } = LastName;

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserEntity, UserListModel>();
        }
    }
}
