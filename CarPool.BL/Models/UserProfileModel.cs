using AutoMapper;
using DAL.Entities;

namespace CarPool.BL.Models;
public record UserProfileModel(
        string FirstName,
        string LastName,
        string PhotoUrl) : ModelBase
{
    public string FirstName { get; } = FirstName;
    public string LastName { get; } = LastName;
    public string PhotoUrl { get; } = PhotoUrl;

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserEntity, UserProfileModel>();
        }
    }
    public static UserProfileModel Empty => new(string.Empty, string.Empty, string.Empty);
}
