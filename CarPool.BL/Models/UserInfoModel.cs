using AutoMapper;
using DAL.Entities;

namespace CarPool.BL.Models;
public record UserInfoModel(
        string FirstName,
        string LastName,
        string PhotoUrl) : ModelBase

{
    public string FirstName { get; set; } = FirstName;
    public string LastName { get; set; } = LastName;
    public string PhotoUrl { get; set; } = PhotoUrl;
    public List<CarInfoModel> Cars { get; init; } = new();

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserEntity, UserInfoModel>()
                .ReverseMap();
        }
    }
    public static UserInfoModel Empty => new(string.Empty, string.Empty,string.Empty);
}
