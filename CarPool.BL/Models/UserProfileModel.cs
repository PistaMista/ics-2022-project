using AutoMapper;
using DAL.Entities;

namespace CarPool.BL.Models;
public record UserProfileModel(
        string FirstName,
        string LastName) : ModelBase
{
    public string FirstName { get; set; } = FirstName;
    public string LastName { get; set; } = LastName;
    public string? PhotoUrl { get; set; }

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserEntity, UserListModel>()
                .ReverseMap();
        }
    }
    public static UserProfileModel Empty => new(string.Empty, string.Empty);
}
