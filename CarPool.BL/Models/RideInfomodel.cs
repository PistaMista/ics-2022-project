using AutoMapper;
using DAL.Entities;
using Common.Enums;

namespace CarPool.BL.Models;
public record RideInfoModel(
    string StartLocation,
    string EndLocation,
    DateTime StartTime,
    uint Duration,
    Guid CarId,
    Guid DriverId) : ModelBase
{

    public string StartLocation { get; set; } = StartLocation;
    public string EndLocation { get; set; } = EndLocation;
    public DateTime StartTime { get; set; } = StartTime;
    public uint Duration { get; set; } = Duration;
    public Guid CarId { get; set; } = CarId;
    public Guid DriverId { get; set; } = DriverId;
    public List<UserModel> Passengers { get; init; } = new();

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<RideEntity,RideInfoModel>();
        }
    }
    public static RideInfoModel Empty => new(string.Empty, string.Empty, default , 0, default, default);
}
