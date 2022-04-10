using AutoMapper;
using CarPool.DAL.Entities;

namespace CarPool.BL.Models;
public record RideInfoModel(
    DateTime StartTime,
    string StartLocation,
    string EndLocation,
    Guid CarId,
    Guid DriverId) : ModelBase
{
    public DateTime StartTime { get; set; } = StartTime;
    public string StartLocation { get; set; } = StartLocation;
    public string EndLocation { get; set; } = EndLocation;
    public Guid CarId { get; set; } = CarId;
    public Guid DriverId { get; set; } = DriverId;

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<RideEntity, RideInfoModel>();
        }
    }
}
