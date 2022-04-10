using AutoMapper;
using CarPool.DAL.Entities;

namespace CarPool.BL.Models;
public record UserInfoModel(
        string FirstName,
        string LastName,
        string PhotoUrl) : ModelBase
{
    public string FirstName { get; set; } = FirstName;
    public string LastName { get; set; } = LastName;
    public string PhotoUrl { get; set; } = PhotoUrl;

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserEntity, UserInfoModel>();
        }
    }
    public static UserInfoModel Empty => new(string.Empty, string.Empty, string.Empty);
}
