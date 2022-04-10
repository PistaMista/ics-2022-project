using AutoMapper;
using DAL.Entities;
using Common.Enums;

namespace CarPool.BL.Models;
public record RideInfoModel(
    string StartLocation,
    string EndLocation,
    DateTime StartTime,
    uint Duration,
    UserProfileModel Driver,
    Guid CarId) : ModelBase
{

    public string StartLocation { get; set; } = StartLocation;
    public string EndLocation { get; set; } = EndLocation;
    public DateTime StartTime { get; set; } = StartTime;
    public uint Duration { get; set; } = Duration;
    public Guid CarId { get; } = CarId;
    public UserProfileModel Driver { get; set; } = Driver;
    public List<UserProfileModel> Passengers { get; init; } = new();

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<RideEntity, RideInfoModel>();
        }
    }
    public static RideInfoModel Empty => new(string.Empty, string.Empty, DateTime.MinValue , 0, UserProfileModel.Empty, Guid.Empty);
}
