using AutoMapper;
using DAL.Entities;

namespace CarPool.BL.Models;
public record UserModel(
        string FirstName,
        string LastName,
        string PhotoUrl) : ModelBase

{
    public string FirstName { get; set; } = FirstName;
    public string LastName { get; set; } = LastName;
    public string PhotoUrl { get; set; } = PhotoUrl;
    public List<CarModel> Cars { get; init; } = new();

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserEntity, UserModel>()
                .ReverseMap();
        }
    }
    public static UserModel Empty => new(string.Empty, string.Empty,string.Empty);
}
