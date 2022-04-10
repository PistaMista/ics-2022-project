using AutoMapper;
using DAL.Entities;
using Common.Enums;

namespace CarPool.BL.Models;
public record RideModel(
    string StartLocation,
    string EndLocation,
    DateTime StartTime,
    uint Duration,
    UserInfoModel Driver,
    Guid CarId) : ModelBase
{

    public string StartLocation { get; set; } = StartLocation;
    public string EndLocation { get; set; } = EndLocation;
    public DateTime StartTime { get; set; } = StartTime;
    public uint Duration { get; set; } = Duration;
    public Guid CarId { get; set; } = CarId;
    public UserInfoModel Driver { get; set; } = Driver;
    public List<UserInfoModel> Passengers { get; init; } = new();

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<RideEntity, RideModel>()
                .ReverseMap();
        }
    }
    public static RideModel Empty => new(string.Empty, string.Empty, DateTime.MinValue , 0, UserInfoModel.Empty, Guid.Empty);
}
